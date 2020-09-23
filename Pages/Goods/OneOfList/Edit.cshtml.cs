using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.Pages.Goods;

namespace Mtd.Cpq.Manager
{
    public class EditModel : PageModel
    {
        private readonly Mtd.Cpq.Manager.Data.CpqContext _context;

        public EditModel(Mtd.Cpq.Manager.Data.CpqContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MtdCpqOneOf MtdCpqOneOf { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MtdCpqOneOf = await _context.MtdCpqOneOfs.FirstOrDefaultAsync(m => m.Id == id);

            if (MtdCpqOneOf == null)
            {
                return NotFound();
            }
            
            HandlerTags handlerTags = new HandlerTags(_context);
            List<TagData> tagList = await handlerTags.GetFreeListAsync(MtdCpqOneOf.ImportTag);                 
            ViewData["TagList"] = new SelectList(tagList, "Id", "Name", MtdCpqOneOf.ImportTag);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MtdCpqOneOf).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MtdCpqOneOfExists(MtdCpqOneOf.Id))
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

        private bool MtdCpqOneOfExists(string id)
        {
            return _context.MtdCpqOneOfs.Any(e => e.Id == id);
        }
    }
}
