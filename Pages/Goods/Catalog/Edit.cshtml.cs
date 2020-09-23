using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.Components;
using Mtd.Cpq.Manager.Data;

namespace Mtd.Cpq.Manager.Pages.Goods.Catalog
{
    public class EditModel : PageModel
    {
        private readonly Mtd.Cpq.Manager.Data.CpqContext _context;

        public EditModel(Mtd.Cpq.Manager.Data.CpqContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MtdCpqCatalog MtdCpqCatalog { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MtdCpqCatalog = await _context.MtdCpqCatalog.FirstOrDefaultAsync(m => m.Id == id);

            if (MtdCpqCatalog == null)
            {
                return NotFound();
            }
            ViewData["Parent"] = new SelectList(_context.MtdCpqCatalog, "Id", "Id");
            
            HandlerTags handlerTags = new HandlerTags(_context);
            List<TagData> tagList = await handlerTags.GetFreeListAsync(MtdCpqCatalog.ImportTag);
            ViewData["TagList"] = new SelectList(tagList, "Id", "Name", MtdCpqCatalog.ImportTag);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MtdCpqCatalog).State = EntityState.Modified;
            MTDImgSModify imgSModify = await MTDImgSelector.ImageModifyAsync(MtdCpqCatalog.Id, Request);
            MtdCpqCatalog.Image = imgSModify.Image;
            _context.Entry(MtdCpqCatalog).Property(x => x.Image).IsModified = imgSModify.Modify;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MtdCpqCatalogExists(MtdCpqCatalog.Id))
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

        private bool MtdCpqCatalogExists(string id)
        {
            return _context.MtdCpqCatalog.Any(e => e.Id == id);
        }
    }
}
