using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mtd.Cpq.Manager.Components;
using Mtd.Cpq.Manager.Data;

namespace Mtd.Cpq.Manager.Pages.Goods.Catalog
{
    public class CreateModel : PageModel
    {
        private readonly Mtd.Cpq.Manager.Data.CpqContext _context;

        public CreateModel(Mtd.Cpq.Manager.Data.CpqContext context)
        {
            _context = context;
        }
        
        [BindProperty]
        public MtdCpqCatalog MtdCpqCatalog { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["Parent"] = new SelectList(_context.MtdCpqCatalog, "Id", "Id");
            HandlerTags handlerTags = new HandlerTags(_context);
            List<TagData> tagList = await handlerTags.GetFreeListAsync();
            ViewData["TagList"] = new SelectList(tagList, "Id", "Name");
            return Page();
        }



        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            MTDImgSModify imgSModify = await MTDImgSelector.ImageModifyAsync("1", Request);
            MtdCpqCatalog.Image = imgSModify.Image;
            MtdCpqCatalog.Id = Guid.NewGuid().ToString();
            _context.MtdCpqCatalog.Add(MtdCpqCatalog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}