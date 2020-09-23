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
    public class IndexModel : PageModel
    {
        private readonly CpqContext _context;

        public IndexModel(CpqContext context)
        {
            _context = context;
        }

        public IList<MtdCpqOneOf> MtdCpqOneOf { get;set; }
        [BindProperty]
        public string SearchText { get; set; }
        public async Task OnGetAsync(string searchText)
        {
            SearchText = searchText;
            MtdCpqOneOf = await _context.MtdCpqOneOfs.ToListAsync();
        }


        public async Task<IActionResult> OnPostDeleteAsync()
        {
            string id = Request.Form["delete-input"];
            MtdCpqOneOf rule = new MtdCpqOneOf { Id = id };
           var upData  = await _context.MtdCpqRuleAvailable.Where(x => x.OneOfId == rule.Id).Select(x => new MtdCpqRuleAvailable
            {
                Id = x.Id, ProductIdParent = x.ProductIdParent, ProductIdChild = x.ProductIdChild, Required = x.Required,
                OneOfId = null

            }).ToListAsync();

            _context.UpdateRange(upData);            
            await _context.SaveChangesAsync();
            _context.Remove(rule);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { searchText = SearchText});
        }
    }
}
