using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.DataConfig;
using Mtd.Cpq.Manager.Services;
using Mtd.OrderMaker.Server.Areas.Identity.Data;
using static Mtd.Cpq.Manager.Services.UserHandler;

namespace Mtd.Cpq.Manager.Pages.Proposal
{
    public class CreateModel : PageModel
    {
        private readonly CpqContext _context;
        private readonly UserHandler _userHandler;
        private readonly IOptions<ConfigSettings> _config;

        public CreateModel(CpqContext context, UserHandler userHandler, IOptions<ConfigSettings> config)
        {
            _context = context;
            _userHandler = userHandler;
            _config = config;
        }


        [BindProperty]
        public MtdCpqProposal MtdCpqProposal { get; set; }
        [BindProperty]
        public string TitleId { get; set; }
        public bool Filter { get; set; }
        public async Task<IActionResult> OnGetAsync(bool filter = false, string pf = "", string tname = "", string tnote = "", string ds = "")
        {

            MtdCpqProposal = new MtdCpqProposal()
            {
                DateCreation = DateTime.Now,
                PreparedFor = pf,
                TitleName = tname,
                TitleNote = tnote,
                Description = ds
            };

            Filter = filter;

            UserParams userParams = await _userHandler.GetTitlesForUserAsync(HttpContext.User);
            IList<MtdCpqTitles> ownList = await _context.MtdCpqTitles.Where(x => userParams.OwnTitleIds.Contains(x.Id)).ToListAsync();
            IQueryable<MtdCpqTitles> queryAccess = _context.MtdCpqTitles.Where(x=> !userParams.OwnTitleIds.Contains(x.Id));
            
            if (userParams.CpqPolicyView == CpqPolicyView.ViewGroup && !Filter)
            {
                queryAccess = queryAccess.Where(x => userParams.GroupTitleIds.Contains(x.Id));
            }
            
            var all = ownList.Select(x => new { x.Id, Name = $"{x.Name} {x.PreparedBy}" }).ToList();
            if (all.Count > 0) { all = all.OrderBy(x => x.Name).ToList(); }

            if (!Filter)
            {
                var defaultList = await queryAccess.Select(x => new { x.Id, Name = $"{x.Name} {x.PreparedBy}" }).ToListAsync();
                if (defaultList.Count > 0) { defaultList = defaultList.OrderBy(x => x.Name).ToList(); }
                all.AddRange(defaultList);   
            }
        

            ViewData["Params"] = new SelectList(all, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var titles = await _context.MtdCpqTitles.Where(x => x.Id == TitleId).FirstOrDefaultAsync();

            MtdCpqCounter dc = await _context.MtdCpqCounter.FindAsync(Constant.GuidProposalCounter);

            _context.Entry(dc).State = EntityState.Modified;
            dc.Counter += 1;
            int counter = dc.Counter;
            await _context.SaveChangesAsync();
            _context.Entry(dc).State = EntityState.Detached;

            if (titles == null) { return NotFound(); }

            WebAppUser user = await _userHandler.GetUserAsync(HttpContext.User);

            MtdCpqProposal.Id = Guid.NewGuid().ToString();

            MtdCpqProposal.Logo = titles.Logo;
            MtdCpqProposal.LogoWidth = titles.LogoWidth <= 0 ? 250 : titles.LogoWidth;
            MtdCpqProposal.LogoHeight = titles.LogoHeight <= 0 ? 100 : titles.LogoHeight;
            MtdCpqProposal.PreparedBy = titles.PreparedBy;
            MtdCpqProposal.ContactName = titles.ContactName;
            MtdCpqProposal.ContactPhone = titles.ContactPhone;
            MtdCpqProposal.ContactEmail = titles.ContactEmail;
            MtdCpqProposal.DateCreation = DateTime.Now;
            MtdCpqProposal.Language = titles.Language;
            MtdCpqProposal.LogoFlexible = titles.LogoFlexible;

            MtdCpqProposal.CustomerCurrency = _config.Value.CultureInfo;

            MtdCpqProposal.IdNumber = counter.ToString("D9");

            bool isOk = await _userHandler.SetQuoteOwnerAsync(MtdCpqProposal.Id, user.Id, Request.HttpContext.User);
            if (isOk)
            {
                _context.MtdCpqProposal.Add(MtdCpqProposal);
                await _context.SaveChangesAsync();
                await _userHandler.AddActionLogAsync(ActionType.CreateQuote, HttpContext.User, MtdCpqProposal.Id);
            }

            return RedirectToPage("./Details", new { id = MtdCpqProposal.Id });
        }


        public IActionResult OnPostFilterAsync(bool filter = false)
        {
            string pf = MtdCpqProposal.PreparedFor;
            string tname = MtdCpqProposal.TitleName;
            string tnote = MtdCpqProposal.TitleNote;
            string ds = MtdCpqProposal.Description;

            return RedirectToPage("./Create", new { filter, pf,tname,tnote,ds });
        }
    }
}