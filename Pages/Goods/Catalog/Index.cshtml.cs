using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.Models;

namespace Mtd.Cpq.Manager.Pages.Goods.Catalog
{
    public class IndexModel : PageModel
    {
        private readonly CpqContext _context;

        public IndexModel(CpqContext context)
        {
            _context = context;
        }

        public IList<MtdCpqCatalog> MtdCpqCatalog { get; set; }
        public Paginator Paginator { get; set; }
        [BindProperty]
        public string SearchText { get; set; }
        [BindProperty]
        public int CPage { get; set; }
        public async Task OnGetAsync(string searchText, int cpage=1)
        {
            CPage = cpage;
            IQueryable<MtdCpqCatalog> query = _context.MtdCpqCatalog.OrderBy(x => x.Sequence);
            if (searchText != null)
            {
                string text = searchText.ToUpper();
                query = query.Where(x => x.IdNumber.ToUpper().Contains(text) || x.Name.ToUpper().Contains(text));
            }

            int count = await query.CountAsync();
            Paginator = new Paginator(CPage,50, count);

            MtdCpqCatalog = await query.Skip(Paginator.Skip).Take(Paginator.Take).ToListAsync();
        }


        public async Task<IActionResult> OnPostDeleteAsync()
        {
            string id = Request.Form["delete-input"];
            MtdCpqCatalog catalog = new MtdCpqCatalog { Id = id };
            _context.Remove(catalog);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index", new { searchText = SearchText, page = CPage });
        }
    }
}
