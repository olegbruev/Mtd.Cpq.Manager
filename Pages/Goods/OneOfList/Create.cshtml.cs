using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.Pages.Goods;

namespace Mtd.Cpq.Manager
{
    public class CreateModel : PageModel
    {
        private readonly CpqContext _context;

        public CreateModel(CpqContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            HandlerTags handlerTags = new HandlerTags(_context);
            List<TagData> tagList = await handlerTags.GetFreeListAsync();
            ViewData["TagList"] = new SelectList(tagList, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public MtdCpqOneOf MtdCpqOneOf { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            MtdCpqOneOf.Id = Guid.NewGuid().ToString();
            _context.MtdCpqOneOfs.Add(MtdCpqOneOf);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}