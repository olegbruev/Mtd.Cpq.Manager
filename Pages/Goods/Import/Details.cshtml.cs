using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.Models;

namespace Mtd.Cpq.Manager.Pages.Goods.Import
{
    public class DetailsModel : PageModel
    {
        private readonly CpqContext _context;
        public DetailsModel(CpqContext context)
        {
            _context = context;
        }

        public MtdCpqImport MtdCpqImport { get; set; }
        public IList<MtdCpqImportData> DataList { get; set; }
        public Paginator Paginator { get; set; }
        public string SearchString { get; set; }
        public int CPage { get; set; }
        public int Action { get; set; }
        public async Task<IActionResult> OnGetAsync(string id, string searchString, int action = 2, int cpage=1)
        {
            if (id == null) { return NotFound(); }
            SearchString = searchString;
            CPage = cpage;
            Action = action;
            MtdCpqImport = await _context.MtdCpqImport.FindAsync(id);
            if (MtdCpqImport == null) { return NotFound(); }

            IQueryable<MtdCpqImportData> query = _context.MtdCpqImportData.Where(x => x.MtdCpqImportId == id && x.Action == action);

            if (SearchString != null)
            {
                string text = SearchString.ToUpper().Replace(" ", "");
                query = query.Where(x => x.Name.ToUpper().Replace(" ", "").Contains(text) || x.IdNumber.ToUpper().Replace(" ", "").Contains(text));
            }

            int count = await query.CountAsync();
            Paginator = new Paginator(CPage, 25, count);
            if (count > Paginator.Size)
            {
                query = query.Skip(Paginator.Skip).Take(Paginator.Take);
            }

            DataList = await query.ToListAsync();

            return Page();
        }


        public async Task<IActionResult> OnPostOpenAsync(string number)
        {
            string result = "null";            
            MtdCpqProduct product = await _context.MtdCpqProduct.Where(x => x.IdNumber.Equals(number)).FirstOrDefaultAsync(); 
            if (product != null)
            {
                result = product.Id;
            }

            return new OkObjectResult(result);
        }
    }
}
