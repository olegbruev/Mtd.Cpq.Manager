using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.DataConfig;
using Mtd.Cpq.Manager.Services;
using Mtd.OrderMaker.Server.Areas.Identity.Data;

namespace Mtd.Cpq.Manager.Pages.Proposal
{
    public class ItemsTotalModel : PageModel
    {
        private readonly CpqContext _context;
        private readonly UserHandler _userHandler;
        private readonly IOptions<ConfigSettings> _config;

        public ItemsTotalModel(CpqContext context, UserHandler userHandler, IOptions<ConfigSettings> config)
        {
            _context = context;
            _userHandler = userHandler;
            _config = config;
        }
        [BindProperty]
        public MtdCpqProposal Proposal { get; set; }
        public CultureInfo CurrencyCatalog { get; set; }
        public decimal GrossPrice { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null) { return NotFound(); }

            Proposal = await _context.MtdCpqProposal.FirstOrDefaultAsync(m => m.Id == id);
            if (Proposal == null) { return NotFound(); }

            /*Access check*/
            bool access = await _userHandler.CheckQuoteEditAsync(Proposal.Id, HttpContext.User);
            if (!access) { return NotFound(); }

            WebAppUser user = await _userHandler.GetUserAsync(HttpContext.User);

            CurrencyCatalog = new CultureInfo(_config.Value.CatalogCulture, false);

            IList<MtdCpqProposalItem> items = await _context.MtdCpqProposalItem
                    .Where(x => x.MtdCpqProposalId == Proposal.Id && x.Selected==1).ToListAsync();
            GrossPrice = 0;
            foreach(var item in items)
            {
                GrossPrice += item.Price * item.Qty;
            }

            if(Proposal.ConfigMasterInluded == 1)
            {
                GrossPrice += Proposal.MasterPrice * Proposal.MasterQty;
            }
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var oldProposal = await _context.MtdCpqProposal.FirstOrDefaultAsync(m => m.Id == Proposal.Id);
            if (oldProposal == null) { return NotFound(); }

            /*Access check*/
            bool access = await _userHandler.CheckQuoteEditAsync(oldProposal.Id, HttpContext.User);
            if (!access) { return NotFound(); }

            WebAppUser user = await _userHandler.GetUserAsync(HttpContext.User);

            oldProposal.CustomerCurrency = Proposal.CustomerCurrency;
            oldProposal.PriceCustomer = Proposal.PriceCustomer;
            oldProposal.DeliveryCondition = Proposal.DeliveryCondition;

            _context.MtdCpqProposal.Update(oldProposal);
            await _context.SaveChangesAsync();

            await _userHandler.AddActionLogAsync(UserHandler.ActionType.EditTotal, User, oldProposal.Id);
            return RedirectToPage("./Details", new { id = oldProposal.Id });
        }
    }
}
