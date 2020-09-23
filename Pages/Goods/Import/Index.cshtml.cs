using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.Pages.Goods.Import.Handler;
using Mtd.Cpq.Manager.Services;
using Mtd.OrderMaker.Server.Areas.Identity.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Mtd.Cpq.Manager.Pages.Goods.Import
{
    public class IndexModel : PageModel
    {
        private readonly CpqContext _context;
        private readonly UserHandler _userHandler;
        public IndexModel(CpqContext context, UserHandler userHandler)
        {
            _userHandler = userHandler;
            _context = context;
        }

        [BindProperty]
        public MtdCpqImport ImportActive { get; set; }
        public List<ImportModel> ImportList { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {

            ImportActive = await _context.MtdCpqImport.Where(x => x.StatusProcess == 1).FirstOrDefaultAsync();

            IList<MtdCpqImport> importData = await _context.MtdCpqImport
                .Where(x => x.StatusProcess > 1)
                .OrderByDescending(x => x.TimeCr).ToListAsync();

            ImportList = new List<ImportModel>();
            foreach (var item in importData)
            {
                string userName = "No data";
                WebAppUser user = await _userHandler.FindByIdAsync(item.UserId);
                if (user != null) { userName = user.Title; }
                ImportList.Add(new ImportModel
                {
                    Id = item.Id,
                    DateTime = item.TimeCr,
                    User = userName,
                    Status = item.StatusText,
                    Process = item.StatusProcess,
                    NoteLoad = item.NoteLoad == 1 ? "YES" : "NOT",
                    OldToArchive = item.OldToArchive == 1 ? "YES" : "NOT",
                    DatasheetLoad = item.DatasheetLoad == 1 ? "YES":"NOT"                    
                });
            }

            return Page();
        }

        public async Task<ActionResult> OnPostImportAsync()
        {
            var importId = Request.Form["form-import-id"];
            var noteLoad = Request.Form["note-checkbox"];
            var dataLoad = Request.Form["datasheet-checkbox"];
            var oldToArchive = Request.Form["archive-checkbox"];

            string id_user = _userHandler.GetUserId(HttpContext.User);
            ImportHandler importHandler = new ImportHandler(_context, id_user);

            try
            {
                //if (!file.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))
                //{
                //    return Page();
                //}

                IFormFile file = Request.Form.Files[0];
                bool note = noteLoad.Equals("true") ? true : false;
                bool dataSheet = dataLoad.Equals("true") ? true : false;
                bool old = oldToArchive.Equals("true") ? true : false;
                await UploadFile(file, importHandler, importId, note, dataSheet, old);
            }
            catch (Exception e)
            {
                MtdCpqImport import = await _context.MtdCpqImport.FindAsync(importId);
                if (import != null)
                {
                    import.StatusProcess = 3;
                    import.StatusText = $"{import.StatusText} Error: {e.Message}";
                    _context.MtdCpqImport.Update(import);
                    await _context.SaveChangesAsync();
                }
            };

            return new OkResult();
        }

        public async Task<IActionResult> OnPostStatusAsync(string id)
        {
            string result = "Start...";
            if (id == null) return new OkObjectResult(result);

            MtdCpqImport import = await _context.MtdCpqImport.FindAsync(id);
            if (import != null && import.StatusText != null)
            {
                result = import.StatusText;
            }

            if (import != null && import.StatusProcess > 1)
            {
                result = "complete";
            }

            return new OkObjectResult(result);
        }

        public async Task<IActionResult> OnPostStopAsync()
        {
            var importId = Request.Form["active-import-id"];

            MtdCpqImport import = await _context.MtdCpqImport.FindAsync(importId);
            import.StatusText = "Import was stoped by user.";
            import.StatusProcess = 3;

            _context.MtdCpqImport.Remove(import);
            await _context.SaveChangesAsync();

            return new OkObjectResult("complete");
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var id = Request.Form["delete-input"];
            MtdCpqImport import = await _context.MtdCpqImport.FindAsync(id);
            if (import != null)
            {
                _context.MtdCpqImport.Remove(import);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        private async Task UploadFile(IFormFile file, ImportHandler importHandler, string importId, bool noteLoad = false, bool dataLoad = false, bool oldToArchive = false)
        {
            MtdCpqImportParam param = await _context.MtdCpqImportParams.FirstOrDefaultAsync();
            if (param == null) { RedirectToPage("./Edit"); }

            if (file == null || file.Length == 0)
            {
                await importHandler.UpdateStatusAsync("File not selected.", 2);
                return;
            }

            ISheet sheet;
            using var stream = file.OpenReadStream();
            stream.Position = 0;
            XSSFWorkbook hssfwb = new XSSFWorkbook(stream);

            await importHandler.StartImportAsync(importId, noteLoad, dataLoad, oldToArchive);

            for (int i = 0; i < hssfwb.NumberOfSheets; i++)
            {
                sheet = hssfwb.GetSheetAt(i);
                await importHandler.UpdateStatusAsync($"Start read sheet {sheet.SheetName}");
                int cellSequence = GetCellData(param, sheet);
                if (cellSequence == -1) continue;

                for (int s = (sheet.FirstRowNum + 1); s <= sheet.LastRowNum; s++)
                {

                    await importHandler.UpdateStatusAsync($"Sheet {sheet.SheetName} reads {i + 1} of {hssfwb.NumberOfSheets} and rows {s} of {sheet.LastRowNum}");

                    IRow row = sheet.GetRow(s);
                    if (row == null) continue;
                    ICell cell = row.GetCell(cellSequence);
                    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                    string tag = cell.StringCellValue;

                    string number = "";
                    ICell cellNumber = row.GetCell(param.ColNumber - 1);
                    if (cellNumber != null && !string.IsNullOrWhiteSpace(cellNumber.ToString()))
                    {
                        if (cellNumber.CellType == CellType.Numeric)
                        {
                            number = cellNumber.NumericCellValue.ToString();
                        }
                        else
                        {
                            number = cellNumber.StringCellValue;
                        }
                    }

                    if (number.Length > 254) { number = number.Substring(0, 254); }

                    string name = "";
                    ICell cellName = row.GetCell(param.ColName - 1);
                    if (cellName != null && !string.IsNullOrWhiteSpace(cellName.ToString()))
                    {
                        name = cellName.StringCellValue;
                    }

                    decimal price = 0;
                    ICell cellPrice = row.GetCell(param.ColPrice - 1);
                    if (cellPrice != null && !string.IsNullOrWhiteSpace(cellPrice.ToString()))
                    {
                        price = (decimal)cellPrice.NumericCellValue;
                    }

                    ICell cellNote = row.GetCell(param.ColNote - 1);
                    string note = null;
                    if (cellNote != null && !string.IsNullOrWhiteSpace(cellNote.ToString()))
                    {
                        note = cellNote.StringCellValue;
                    }

                    ICell cellData = row.GetCell(param.ColData - 1);
                    string datasheet = null;
                    if (cellData != null && !string.IsNullOrWhiteSpace(cellData.ToString()))
                    {
                        datasheet = cellData.StringCellValue;
                    }

                    importHandler.AddImportData(tag, number, name, note, datasheet, price);
                }
            }

            await importHandler.UpdateStatusAsync("Save import log.");
            await importHandler.SaveImportDataAsync();
            await importHandler.UpdateAllAsync();
            await importHandler.UpdateStatusAsync("Import full complete.", 2);
        }

        private static int GetCellData(MtdCpqImportParam param, ISheet sheet)
        {
            int cellSequence = -1;
            IRow headerRow = sheet.GetRow(0);

            if (headerRow == null) return cellSequence;

            int cellCount = headerRow.LastCellNum;
            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                if (cell.StringCellValue.Equals(param.TagData))
                {
                    cellSequence = j;
                    break;
                }
            }

            return cellSequence;
        }
    }


    public class ImportModel
    {
        public string Id { get; set; }
        public DateTime DateTime { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
        public int Process { get; set; }
        public string NoteLoad { get; set; }
        public string OldToArchive { get; set; }
        public string DatasheetLoad { get; set; }
    }
}