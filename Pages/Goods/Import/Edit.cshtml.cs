using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.Data;

namespace Mtd.Cpq.Manager.Pages.Goods.Import
{
    public class EditModel : PageModel
    {
        private readonly CpqContext _context;

        public EditModel(CpqContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MtdCpqImportParam MtdCpqImportParam { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            MtdCpqImportParam = await _context.MtdCpqImportParams.FirstOrDefaultAsync();

            if (MtdCpqImportParam == null) { await CreateParamAsync(); }

            HandlerTags handlerTags = new HandlerTags(_context);
            List<TagData> tagData = await handlerTags.GetFreeListAsync(MtdCpqImportParam.TagData);
            ViewData["TagData"] = new SelectList(tagData, "Id", "Name", MtdCpqImportParam.TagData);
            
            List<TagData> tagMaster = await handlerTags.GetFreeListAsync(MtdCpqImportParam.TagMaster);
            ViewData["TagMaster"] = new SelectList(tagMaster, "Id", "Name", MtdCpqImportParam.TagMaster);

            List<TagData> tagRequired = await handlerTags.GetFreeListAsync(MtdCpqImportParam.TagRequired);
            ViewData["TagRequired"] = new SelectList(tagRequired, "Id", "Name", MtdCpqImportParam.TagRequired);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
         
            _context.Attach(MtdCpqImportParam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MtdCpqImportParamExists(MtdCpqImportParam.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MtdCpqImportParamExists(string id)
        {
            return _context.MtdCpqImportParams.Any(e => e.Id == id);
        }


        private async Task CreateParamAsync()
        {

            MtdCpqImportParam = new MtdCpqImportParam
            {
                Id = Guid.NewGuid().ToString(),
                TagData = "@",
                ColNumber = 2,
                ColName = 3,
                ColPrice = 4,
                ColNote = 50,
                TagMaster = "#",
                TagRequired = "!",
                ColData = 51
            };
            await _context.MtdCpqImportParams.AddAsync(MtdCpqImportParam);
            await _context.SaveChangesAsync();
        }
    }
}
