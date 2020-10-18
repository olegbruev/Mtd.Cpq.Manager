using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mtd.Cpq.Manager.Data;

namespace Mtd.Cpq.Manager.Pages.Admin.Notification
{
    public class CreateModel : PageModel
    {
        private readonly CpqContext _context;

        public CreateModel(CpqContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MtdCpqNotification MtdCpqNotification { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            MtdCpqNotification notification = new MtdCpqNotification
            {
                Id = Guid.NewGuid().ToString(),
                TimeCr = DateTime.Now,
                UserName = User.Identity.Name,
                Title = MtdCpqNotification.Title,
                Message = MtdCpqNotification.Message

            };

            _context.MtdCpqNotifications.Add(notification);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
