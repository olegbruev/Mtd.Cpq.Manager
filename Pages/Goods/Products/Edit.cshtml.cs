using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.Components;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.Services;

namespace Mtd.Cpq.Manager.Pages.Goods.Products
{
    public class EditModel : PageModel
    {
        private readonly CpqContext _context;
        private readonly UserHandler userHandler;

        public EditModel(CpqContext context, UserHandler userHandler)
        {
            _context = context;
            this.userHandler = userHandler;
        }

        [BindProperty]
        public MtdCpqProduct MtdCpqProduct { get; set; }
        
        [BindProperty]
        public bool Archive { get; set; }
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

            Archive = MtdCpqProduct.Archive == 1 ? true : false;
            ViewData["MtdCpqCatalogId"] = new SelectList(_context.MtdCpqCatalog, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string cpage = Request.Form["get-cpage"];
            string archive = Request.Form["get-archive"];
            string searchText = Request.Form["get-searchText"];
            string master = Request.Form["get-master"];


            MtdCpqProduct.Archive = Archive ? (sbyte)1 : (sbyte)0;

            _context.Attach(MtdCpqProduct).State = EntityState.Modified;

            bool som = await Components.MTDCheckbox.GetResult("master-product", Request);
            MtdCpqProduct.Som = som ? (sbyte)1 : (sbyte)0;

            if (!som) { master = null; }

            MTDImgSModify imgSModify = await MTDImgSelector.ImageModifyAsync(MtdCpqProduct.Id, Request);
            MtdCpqProduct.Image = imgSModify.Image;
            _context.Entry(MtdCpqProduct).Property(x => x.Image).IsModified = imgSModify.Modify;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MtdCpqProductExists(MtdCpqProduct.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            await userHandler.AddActionLogAsync(UserHandler.ActionType.ProductEdit, HttpContext.User, MtdCpqProduct.Id);

            return RedirectToPage("./Index", new {catalog=MtdCpqProduct.MtdCpqCatalogId,cpage,archive,searchText, master});
        }

        private bool MtdCpqProductExists(string id)
        {
            return _context.MtdCpqProduct.Any(e => e.Id == id);
        }
    }
}