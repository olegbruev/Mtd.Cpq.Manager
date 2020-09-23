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
    public class DetailsModel : PageModel
    {
        private readonly Mtd.Cpq.Manager.Data.CpqContext _context;

        public DetailsModel(Mtd.Cpq.Manager.Data.CpqContext context)
        {
            _context = context;
        }

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
    }
}
