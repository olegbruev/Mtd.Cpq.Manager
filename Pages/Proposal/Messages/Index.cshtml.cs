using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.Models;

namespace Mtd.Cpq.Manager.Pages.Proposal.Messages
{
    public class UserMessage
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool Readed { get; set; }

        public DateTime TimeCr { get; set; }
    }

    public class IndexModel : PageModel
    {
        private readonly CpqContext _context;

        public IndexModel(CpqContext context)
        {
            _context = context;
        }

        public IList<UserMessage> UserMessages { get; set; }

        public Paginator Paginator { get; set; }
        public int CPage { get; set; }
        public string SearchText { get; set; }

        public async Task<IActionResult> OnGetAsync(string searchText, int cpage = 1)
        {

            CPage = cpage < 1 ? 1 : cpage;
            SearchText = searchText;
            int count = 0;

            var query = from notice in _context.MtdCpqNotifications
                        join userInfo in _context.MtdCpqReaderUsers on notice.Id equals userInfo.MessageId into g
                        from userInfo in g.DefaultIfEmpty()
                        where userInfo.UserName == User.Identity.Name || userInfo.UserName == null
                        select new { notice, userInfo };

            if (searchText != null)
            {
                string text = searchText.ToUpper();
                query = query.Where(x => x.notice.Message.ToUpper().Contains(text) || x.notice.Title.ToUpper().Contains(text));
            }

            count = await query.CountAsync();
            Paginator = new Paginator(CPage, 10, count);
            query = query.OrderByDescending(x => x.notice.TimeCr)
                .Skip(Paginator.Skip)
                .Take(Paginator.Take);

            UserMessages = await query.Select(x => new UserMessage
            {
                Id = x.notice.Id,
                Title = x.notice.Title,
                Message = x.notice.Message,
                TimeCr = x.notice.TimeCr,
                Readed = x.userInfo.Id != null
            }).ToListAsync();

            return Page();
        }


        public async Task<IActionResult> OnPostReadAsync()
        {
            var requestForm = await Request.ReadFormAsync();
            string messageId = requestForm["messageId"];

            MtdCpqReaderUser readerUser = new MtdCpqReaderUser
            {
                Id = Guid.NewGuid().ToString(),
                MessageId = messageId,
                TimeCr = DateTime.Now,
                UserName = User.Identity.Name
            };

            await _context.MtdCpqReaderUsers.AddAsync(readerUser);
            await _context.SaveChangesAsync();

            return new OkObjectResult("ok");
        }
    }
}
