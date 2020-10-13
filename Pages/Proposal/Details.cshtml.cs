using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlToOpenXml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Mtd.Cpq.Manager.Common;
using Mtd.Cpq.Manager.Common.DocumentHandler;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.DataConfig;
using Mtd.Cpq.Manager.Services;
using Mtd.OrderMaker.Server.Areas.Identity.Data;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;

namespace Mtd.Cpq.Manager.Pages.Proposal
{
    public class DetailsModel : PageModel
    {
        private readonly CpqContext _context;
        private readonly UserHandler _userHandler;
        private readonly IOptions<ConfigSettings> _config;
        private readonly IOptions<PersonalMenu> personalMenu;

        public DetailsModel(CpqContext context, UserHandler userHandler, IOptions<ConfigSettings> config, IOptions<PersonalMenu> personalMenu)
        {
            _context = context;
            _userHandler = userHandler;
            _config = config;
            this.personalMenu = personalMenu;
        }

        [BindProperty]
        public MtdCpqProposal MtdCpqProposal { get; set; }
        public IList<MtdCpqProposalItem> Items { get; set; }
        public IList<MtdCpqProposalCatalog> Catalogs { get; set; }
        public CultureInfo CatalogCulture { get; set; }
        public CultureInfo CultureView { get; set; }
        public bool PrintGrossPrice { get; set; }
        public bool IsEditor { get; set; }

        public List<string> MasterNotes { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MtdCpqProposal = await _context.MtdCpqProposal
                .FirstOrDefaultAsync(m => m.Id == id);

            if (MtdCpqProposal == null)
            {
                return NotFound();
            }

            /*Access check*/
            bool access = await _userHandler.CheckQuoteAccessAsync(MtdCpqProposal.Id, HttpContext.User);
            if (!access) { return NotFound(); }

            IsEditor = await _userHandler.CheckQuoteEditAsync(MtdCpqProposal.Id, HttpContext.User);

            WebAppUser user = await _userHandler.GetUserAsync(HttpContext.User);

            Items = await _context.MtdCpqProposalItem
                .Where(x => x.MtdCpqProposalId == MtdCpqProposal.Id && x.Selected == 1)
                .OrderBy(x => x.Sequence)
                .ToListAsync();
            Catalogs = await _context.MtdCpqProposalCatalogs
                .Where(x => x.MtdCpqProposalId == MtdCpqProposal.Id)
                .OrderBy(x => x.Sequence)
                .ToListAsync();

            CatalogCulture = new CultureInfo(_config.Value.CatalogCulture, false);
            string cultureInfo = MtdCpqProposal.Language ?? _config.Value.CultureInfo;
            CultureView = new CultureInfo(cultureInfo, false);
            UserHandler.UserParams userParams = _userHandler.GetCpqPolicyAsync(User);
            PrintGrossPrice = userParams.PrintGrossPrice;

            ViewData["GrossPrice"] = personalMenu.Value.GrossPrice;

            MasterNotes = new List<string>();
            if (MtdCpqProposal.MasterNote != null && MtdCpqProposal.MasterNote.Length > 0)
            {
                Regex regex = new Regex(@"(.*?)(?:(\r\n){2,}|\r{2,}|\n{2,}|$)", RegexOptions.Multiline);
                MatchCollection matches = regex.Matches(MtdCpqProposal.MasterNote);
                foreach (Match match in matches) { MasterNotes.Add(match.Value); }
            }


            return Page();
        }


        public async Task<IActionResult> OnPostEditItemsAsync(string id)
        {
            var MtdCpqProposal = await _context.MtdCpqProposal.FirstOrDefaultAsync(m => m.Id == id);

            if (MtdCpqProposal == null) { return NotFound(); }

            /*Access check*/
            bool access = await _userHandler.CheckQuoteEditAsync(MtdCpqProposal.Id, HttpContext.User);
            if (!access) { return NotFound(); }

            WebAppUser user = await _userHandler.GetUserAsync(HttpContext.User);

            if (MtdCpqProposal.MasterId != null)
            {
                return RedirectToPage("./ItemsEdit", new { id = MtdCpqProposal.Id });
            }

            return RedirectToPage("./ItemsCreate", new { proposal = MtdCpqProposal.Id });
        }

        public async Task<IActionResult> OnPostViewSetAsync()
        {
            MtdCpqProposal proposal = await _context.MtdCpqProposal.FirstOrDefaultAsync(m => m.Id == MtdCpqProposal.Id);
            if (proposal == null) { return NotFound(); }

            /*Access check*/
            bool access = await _userHandler.CheckQuoteAccessAsync(proposal.Id, HttpContext.User);
            if (!access) { return NotFound(); }
            WebAppUser user = await _userHandler.GetUserAsync(HttpContext.User);


            proposal.ViewNote = MtdCpqProposal.ViewNote;
            proposal.ViewPriceCustomer = MtdCpqProposal.ViewPriceCustomer;
            proposal.ViewPriceGross = MtdCpqProposal.ViewPriceGross;
            proposal.ViewDelivery = MtdCpqProposal.ViewDelivery;
            proposal.ViewQty = MtdCpqProposal.ViewQty;
            proposal.ViewImages = MtdCpqProposal.ViewImages;
            proposal.ViewDatasheet = MtdCpqProposal.ViewDatasheet;
            proposal.ViewProposal = MtdCpqProposal.ViewProposal;


            _context.MtdCpqProposal.Update(proposal);
            await _context.SaveChangesAsync();

            return new OkObjectResult("OK");
        }

        public async Task<IActionResult> OnPostExcelAsync(string id)
        {
            if (id == null) { return NotFound(); }

            MtdCpqProposal oldData = await _context.MtdCpqProposal
                .AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (oldData == null) { return NotFound(); }

            /*Access check*/
            bool access = await _userHandler.CheckQuoteAccessAsync(oldData.Id, HttpContext.User);
            if (!access) { return NotFound(); }

            WebAppUser user = await _userHandler.GetUserAsync(HttpContext.User);

            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("Sheet1");
            var rowIndex = 0;
            IRow rowTitle = sheet1.CreateRow(rowIndex);
            rowTitle.CreateCell(0).SetCellValue("Part Number");
            rowTitle.CreateCell(1).SetCellValue("Items Description");
            rowTitle.CreateCell(2).SetCellValue("Qty");
            rowTitle.CreateCell(3).SetCellValue("Cost");

            if (oldData.ConfigMasterInluded == 1)
            {
                rowIndex++;
                IRow rowMaster = sheet1.CreateRow(rowIndex);
                rowMaster.CreateCell(0).SetCellValue(oldData.MasterNumber);
                rowMaster.CreateCell(1).SetCellValue(oldData.MasterName);
                rowMaster.CreateCell(2).SetCellValue(oldData.MasterQty);
                rowMaster.CreateCell(3).SetCellValue((double)oldData.MasterPrice);
                if (oldData.MasterNote != null && oldData.MasterNote.Length > 0)
                {
                    rowIndex++;
                    IRow rowMasterNote = sheet1.CreateRow(rowIndex);
                    rowMasterNote.CreateCell(0).SetCellValue("");
                    rowMasterNote.CreateCell(1).SetCellValue(oldData.MasterNote);
                    rowMasterNote.CreateCell(2).SetCellValue("");
                    rowMasterNote.CreateCell(3).SetCellValue("");
                }

            }

            Items = await _context.MtdCpqProposalItem
                .Include(x => x.MtdCpqProposalCatalog)
                .Where(x => x.MtdCpqProposalId == oldData.Id && x.Selected == 1)
                .OrderBy(x => x.MtdCpqProposalCatalog.Sequence)
                .ToListAsync();

            var catalogs = Items.GroupBy(x => x.ProposalCatalogId).Select(x => x.Key);

            foreach (var catalog in catalogs)
            {

                var items = Items.Where(x => x.ProposalCatalogId == catalog);
                if (items.Any())
                {
                    rowIndex++;
                    IRow rowCatalog = sheet1.CreateRow(rowIndex);
                    rowCatalog.CreateCell(0).SetCellValue(Items.FirstOrDefault().MtdCpqProposalCatalog.Name);

                    foreach (var item in items)
                    {
                        rowIndex++;
                        IRow row = sheet1.CreateRow(rowIndex);
                        row.CreateCell(0).SetCellValue(item.IdNumber);
                        row.CreateCell(1).SetCellValue(item.Name);
                        row.CreateCell(2).SetCellValue(item.Qty);
                        row.CreateCell(3).SetCellValue((double)item.Price);
                        if (item.Note != null && item.Note.Length > 0)
                        {
                            rowIndex++;
                            IRow rowNote = sheet1.CreateRow(rowIndex);
                            rowNote.CreateCell(0).SetCellValue("");
                            rowNote.CreateCell(1).SetCellValue(item.Note);
                            rowNote.CreateCell(2).SetCellValue("");
                            rowNote.CreateCell(3).SetCellValue("");
                        }

                    }
                }
            }

            sheet1.AutoSizeColumn(0);
            workbook.WriteExcelToResponse(HttpContext, "cpq.xlsx");

            return RedirectToPage("./Details", new { id = oldData.Id });
        }

        public async Task<IActionResult> OnPostWordAsync(string id)
        {
            MtdCpqProposal oldData = await _context.MtdCpqProposal
                    .AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (oldData == null) { return NotFound(); }

            /*Access check*/
            bool access = await _userHandler.CheckQuoteAccessAsync(oldData.Id, HttpContext.User);
            if (!access) { return NotFound(); }

            WebAppUser user = await _userHandler.GetUserAsync(HttpContext.User);


            string htmlText = Request.Form["input-data"];

            using MemoryStream mem = new MemoryStream();
            using (WordprocessingDocument wordDoc = WordprocessingDocument
                .Create(mem, DocumentFormat.OpenXml.WordprocessingDocumentType.Document, true))
            {

                PageMargin pageMargin = new PageMargin
                {
                    Top = 150,
                    Bottom = 150,
                    Left = 450,
                    Right = 450
                };

                SectionProperties sectionProps = new SectionProperties();
                sectionProps.Append(pageMargin);
                wordDoc.AddMainDocumentPart();
                // siga a ordem
                DocumentFormat.OpenXml.Wordprocessing.Document doc = new DocumentFormat.OpenXml.Wordprocessing.Document();
                Body body = new Body();
                body.Append(sectionProps);
                doc.Append(body);
                wordDoc.MainDocumentPart.Document = doc;
                HtmlConverter converter = new HtmlConverter(wordDoc.MainDocumentPart);
                converter.ParseHtml(htmlText);
                wordDoc.Close();
            }

            return File(mem.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "ABC.docx");

        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {

            var form = await HttpContext.Request.ReadFormAsync();
            string id = form["delete-input"];


            MtdCpqProposal oldData = await _context.MtdCpqProposal.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (oldData == null) { return NotFound(); }

            WebAppUser user = await _userHandler.GetUserAsync(HttpContext.User);

            /*Access check*/
            bool access = await _userHandler.CheckQuoteEditAsync(oldData.Id, HttpContext.User);
            if (!access) { return NotFound(); }

            _context.MtdCpqProposal.Remove(oldData);
            await _context.SaveChangesAsync();
            await _userHandler.RemoveQuotesOwnerAsync(oldData.Id, HttpContext.User);
            await _userHandler.AddActionLogAsync(UserHandler.ActionType.RemoveQuote, HttpContext.User, oldData.Id);

            return RedirectToPage("./Index");
        }
    }
}
