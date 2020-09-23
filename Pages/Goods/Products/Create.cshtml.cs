using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mtd.Cpq.Manager.Components;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.Services;

namespace Mtd.Cpq.Manager.Pages.Goods.Products
{
    public class CreateModel : PageModel
    {
        private readonly CpqContext _context;
        private readonly UserHandler userHandler;

        public CreateModel(CpqContext context, UserHandler userHandler)
        {
            _context = context;
            this.userHandler = userHandler;
        }

        public IActionResult OnGet()
        {
        ViewData["MtdCpqCatalogId"] = new SelectList(_context.MtdCpqCatalog, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public MtdCpqProduct MtdCpqProduct { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            MTDImgSModify imgSModify = await MTDImgSelector.ImageModifyAsync("1", Request);
            MtdCpqProduct.Image = imgSModify.Image;
            MtdCpqProduct.Id = Guid.NewGuid().ToString();
            _context.MtdCpqProduct.Add(MtdCpqProduct);
            await _context.SaveChangesAsync();
            await userHandler.AddActionLogAsync(UserHandler.ActionType.ProductCreate, HttpContext.User, MtdCpqProduct.Id);
            return RedirectToPage("./Index", new { catalog = MtdCpqProduct.MtdCpqCatalogId});
        }
    }
}