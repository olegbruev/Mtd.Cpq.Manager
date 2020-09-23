using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.Data;

namespace Mtd.Cpq.Manager
{
    public class DeleteModel : PageModel
    {
        private readonly Mtd.Cpq.Manager.Data.CpqContext _context;

        public DeleteModel(Mtd.Cpq.Manager.Data.CpqContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MtdCpqOneOf = await _context.MtdCpqOneOfs.FindAsync(id);

            if (MtdCpqOneOf != null)
            {
                _context.MtdCpqOneOfs.Remove(MtdCpqOneOf);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
