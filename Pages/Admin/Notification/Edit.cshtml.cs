using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.Data;

namespace Mtd.Cpq.Manager.Pages.Admin.Notification
{
    public class EditModel : PageModel
    {
        private readonly Mtd.Cpq.Manager.Data.CpqContext _context;

        public EditModel(Mtd.Cpq.Manager.Data.CpqContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MtdCpqNotification MtdCpqNotification { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MtdCpqNotification = await _context.MtdCpqNotifications.FirstOrDefaultAsync(m => m.Id == id);

            if (MtdCpqNotification == null)
            {
                return NotFound();
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MtdCpqNotification).State = EntityState.Modified;
            _context.Entry(MtdCpqNotification).Property(x => x.UserName).IsModified = false;
            _context.Entry(MtdCpqNotification).Property(x => x.TimeCr).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MtdCpqNotificationExists(MtdCpqNotification.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MtdCpqNotificationExists(string id)
        {
            return _context.MtdCpqNotifications.Any(e => e.Id == id);
        }
    }
}
