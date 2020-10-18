using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.DataConfig;
using Mtd.Cpq.Manager.Models;
using Mtd.Cpq.Manager.Models.Proposal;
using Mtd.Cpq.Manager.Services;
using Mtd.OrderMaker.Server.Areas.Identity.Data;
using static Mtd.Cpq.Manager.Services.UserHandler;

namespace Mtd.Cpq.Manager.Pages.Proposal
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

        public IList<ProposalsList> ProposalsItems { get; set; }

        public Paginator Paginator { get; set; }
        public int CPage { get; set; }
        public string SearchText { get; set; }
        public bool IsAdmin { get; set; }
        public bool Filter { get; set; }
        public async Task<IActionResult> OnGetAsync(string searchText, int cpage = 1, bool filter = false)
        {
            CPage = cpage < 1 ? 1 : cpage;
            SearchText = searchText;
            Filter = filter;

            int count = 0;
            IList<MtdCpqProposal> tempList;
            //bool onlyOwn = Filter == "on" ? true : false;
            UserParams userParams = await _userHandler.GetQuotesForUserAsync(HttpContext.User, Filter);
            IQueryable<MtdCpqProposal> queryAccess = _context.MtdCpqProposal;

            /*Access check*/
            if (userParams.CpqPolicyView != CpqPolicyView.ViewAll || Filter)
            {
                queryAccess = queryAccess.Where(x => userParams.GroupTitleIds.Contains(x.Id));
            }
            
            if (searchText == null)
            {                
                count = await queryAccess.CountAsync();
                Paginator = new Paginator(CPage, 10, count);
                tempList = await queryAccess.OrderByDescending(x=>x.DateCreation)
                    .Skip(Paginator.Skip)
                    .Take(Paginator.Take)
                    .ToListAsync();
            }
            else
            {
                IQueryable<MtdCpqProposal> filterQuery;
                string text = searchText.ToUpper();
                filterQuery = queryAccess.Where(x => 
                x.IdNumber.ToUpper().Contains(text) 
                || x.PreparedFor.ToUpper().Contains(text)
                || x.ContactName.ToUpper().Contains(text)
                || x.MasterNumber.ToUpper().Contains(text)
                || x.MasterName.ToUpper().Contains(text)
                || x.PreparedFor.ToUpper().Contains(text)
                || x.Description.ToUpper().Contains(text)
                );

                IList<string> userIds = await _userHandler.Users.Where(x => x.Title.ToUpper().Contains(text) || x.TitleGroup.ToUpper().Contains(text)).Select(x => x.Id).ToListAsync();
                if (userIds != null)
                {
                    IList<string> ids = await _userHandler.GetQuotesIdsForUsersAsync(userIds);
                    filterQuery = filterQuery.Union(queryAccess.Where(x => ids.Contains(x.Id)));
                }

                count = await filterQuery.CountAsync();

                Paginator = new Paginator(CPage, 10, count);
                tempList = await filterQuery.OrderByDescending(x => x.IdNumber).ThenByDescending(x=>x.DateCreation)
                    .Skip(Paginator.Skip)
                    .Take(Paginator.Take)
                    .ToListAsync();
            }

            IList<string> quotesIds = tempList.Select(x => x.Id).ToList();
            Dictionary<string, WebAppUser> dictionary = await _userHandler.GetOwnersQuotesAsync(quotesIds);

            ProposalsItems = tempList.Select(x => new ProposalsList
            {
                Id = x.Id,
                ContactName = x.ContactName,
                Date = x.DateCreation.ToShortDateString(),
                Description = x.Description,
                MasterImage = x.MasterImage,
                Logo = x.Logo,
                MasterName = x.MasterName,
                MasterNumber = x.MasterNumber,
                Number = x.IdNumber,
                PreparedFor = x.PreparedFor,
                OwnerName = dictionary.Where(d => d.Key == x.Id).Any() ? dictionary.FirstOrDefault(d => d.Key == x.Id).Value.Title : "No owner",
                OwnerGroup = dictionary.Where(d => d.Key == x.Id).Any() ? dictionary.FirstOrDefault(d => d.Key == x.Id).Value.TitleGroup : ""

            }).ToList();

            WebAppUser appUser = await _userHandler.GetUserAsync(HttpContext.User);
            IsAdmin = await _userHandler.IsInRoleAsync(appUser, SystemRoles.Admin);
            return Page();
        }
        

    }
}
