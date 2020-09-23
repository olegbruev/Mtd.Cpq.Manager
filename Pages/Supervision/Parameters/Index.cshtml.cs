using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.Models;
using Mtd.Cpq.Manager.Services;
using Mtd.OrderMaker.Server.Areas.Identity.Data;
using Mtd.Cpq.Manager.Models.Supervision;
using Mtd.Cpq.Manager.DataConfig;
using static Mtd.Cpq.Manager.Services.UserHandler;

namespace Mtd.Cpq.Manager.Pages.Supervision.Parameters
{
    public class IndexModel : PageModel
    {
        private readonly CpqContext _context;
        private readonly UserHandler _userHandler;

        public IndexModel(CpqContext context, UserHandler userHandler)
        {
            _context = context;
            _userHandler = userHandler;
        }
        
        public IList<TitlesItem> TitlesList { get; set; }
        public string SearchText { get; set; }
        public int CPage { get; set; }
        public Paginator Paginator { get; set; }
        public bool IsAdmin { get; set; }
        public bool Filter { get; set; }
        public async Task<IActionResult> OnGetAsync(string searchText, int cpage = 1, bool filter = false)
        {
            CPage = cpage < 1 ? 1 : cpage;
            SearchText = searchText;
            Filter = filter;
            int count = 0;

            IList<MtdCpqTitles> TempList;            
            UserParams userParams = await _userHandler.GetTitlesForUserAsync(HttpContext.User);

            IQueryable<MtdCpqTitles> queryAccess = _context.MtdCpqTitles;

            /*Access check*/
            if (userParams.CpqPolicyView != CpqPolicyView.ViewAll || Filter)
            {
                queryAccess = queryAccess.Where(x => userParams.GroupTitleIds.Contains(x.Id) && userParams.OwnTitleIds.Contains(x.Id));
            }

            if (searchText == null)
            {
                count = await queryAccess.CountAsync();
                Paginator = new Paginator(CPage, 10, count);
                TempList = await queryAccess.OrderBy(x=>x.Name)             
                    .Skip(Paginator.Skip)
                    .Take(Paginator.Take)
                    .ToListAsync();
            }
            else
            {
                IQueryable<MtdCpqTitles> filterQuery;
                string text = searchText.ToUpper();
                filterQuery = queryAccess.Where(x => x.Name.ToUpper().Contains(text)
                    || x.PreparedBy.ToUpper().Contains(text)
                    || x.ContactName.ToUpper().Contains(text));

                IList<string> userIds = await _userHandler.Users.Where(x => x.Title.ToUpper().Contains(text) || x.TitleGroup.ToUpper().Contains(text)).Select(x=>x.Id).ToListAsync();
                if (userIds != null)
                {
                    IList<string> titlesIds =  await _userHandler.GetTitlesIdsForUsersAsync(userIds);
                    filterQuery = filterQuery.Union(queryAccess.Where(x => titlesIds.Contains(x.Id)));
                }

                count = await filterQuery.CountAsync();

                Paginator = new Paginator(CPage, 10, count);
                TempList = await filterQuery.OrderBy(x => x.Name)
                    .Skip(Paginator.Skip)
                    .Take(Paginator.Take)
                    .ToListAsync();
            }

            IList<string> ids = TempList.Select(x => x.Id).ToList();
            Dictionary<string, WebAppUser> dictonary = await _userHandler.GetOwnersTitlesAsync(ids);

            TitlesList = TempList.Select(x => 
                    new TitlesItem { 
                        Id = x.Id, 
                        Logo = x.Logo, 
                        Name = x.Name, 
                        PreparedBy = x.PreparedBy, 
                        ContactName = x.ContactName, 
                        Owner = GetLongUserName(dictonary,x.Id)
                    }).ToList();

            IsAdmin = User.IsInRole(SystemRoles.Admin);
            return Page();
        }
         
        private string GetLongUserName (Dictionary<string, WebAppUser> dictonary, string key)
        {
            if (!dictonary.Where(f => f.Key == key).Any()) { return "No owner"; }
            WebAppUser appUser = dictonary.FirstOrDefault(f => f.Key == key).Value;
            return $"{appUser.Title} {appUser.TitleGroup}";
        }

    }
}
