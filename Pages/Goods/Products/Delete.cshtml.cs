using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.Data;

namespace Mtd.Cpq.Manager.Pages.Goods.Products
{
    public class DeleteModel : PageModel
    {
        private readonly Mtd.Cpq.Manager.Data.CpqContext _context;

        public DeleteModel(Mtd.Cpq.Manager.Data.CpqContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MtdCpqProduct MtdCpqProduct { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MtdCpqProduct = await _context.MtdCpqProduct
                .Include(m => m.MtdCpqCatalog).FirstOrDefaultAsync(m => m.Id == id);

            if (MtdCpqProduct == null)
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

            MtdCpqProduct = await _context.MtdCpqProduct.FindAsync(id);

            if (MtdCpqProduct != null)
            {
                _context.MtdCpqProduct.Remove(MtdCpqProduct);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new { id = MtdCpqProduct.MtdCpqCatalogId });
        }
    }
}
