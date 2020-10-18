using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.Models;

namespace Mtd.Cpq.Manager.Pages.Admin.Notification
{
    public class IndexModel : PageModel
    {
        private readonly CpqContext _context;

        public IndexModel(CpqContext context)
        {
            _context = context;
        }

        public IList<MtdCpqNotification> MtdCpqNotification { get;set; }

        public Paginator Paginator { get; set; }
        public int CPage { get; set; }
        public string SearchText { get; set; }

        public async Task OnGetAsync(string searchText, int cpage = 1)
        {
            CPage = cpage < 1 ? 1 : cpage;
            SearchText = searchText;
            int count = 0;
            IQueryable<MtdCpqNotification> query = _context.MtdCpqNotifications;

            if (searchText != null)
            {
                string text = searchText.ToUpper();
                query = query.Where(x => x.Title.ToUpper().Contains(text) || x.Message.ToUpper().Contains(text));      
            }
            
            count = await query.CountAsync();
            Paginator = new Paginator(CPage, 10, count);
            query = query.OrderByDescending(x => x.TimeCr)
                .Skip(Paginator.Skip)
                .Take(Paginator.Take);

            MtdCpqNotification = await query.ToListAsync();
        }


        public async Task<IActionResult> OnPostDeleteAsync()
        {

            var form = await HttpContext.Request.ReadFormAsync();
            string id = form["delete-input"];


            MtdCpqNotification notification = await _context.MtdCpqNotifications.FindAsync(id);

            if (notification == null) { return NotFound(); }


            _context.MtdCpqNotifications.Remove(notification);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
