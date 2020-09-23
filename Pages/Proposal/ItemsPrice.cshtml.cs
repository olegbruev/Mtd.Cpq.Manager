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
    public class ItemsPriceModel : PageModel
    {
        private readonly CpqContext _context;
        private readonly UserHandler _userHandler;
        private readonly IOptions<ConfigSettings> _config;

        public ItemsPriceModel(CpqContext context, UserHandler userHandler, IOptions<ConfigSettings> config)
        {
            _context = context;
            _userHandler = userHandler;
            _config = config;
        }

        [BindProperty]
        public MtdCpqProposal MtdCpqProposal { get; set; }

        [BindProperty]
        public IList<MtdCpqProposalItem> Items { get; set; }
        public IList<MtdCpqProposalCatalog> Catalogs { get; set; }
        [BindProperty]
        public CultureInfo CatalogCulture { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null) { return NotFound(); }

            MtdCpqProposal = await _context.MtdCpqProposal.FirstOrDefaultAsync(m => m.Id == id);
            if (MtdCpqProposal == null) { return NotFound(); }

            /*Access check*/
            bool access = await _userHandler.CheckQuoteEditAsync(MtdCpqProposal.Id, HttpContext.User);
            if (!access) { return NotFound(); }

            WebAppUser user = await _userHandler.GetUserAsync(HttpContext.User);

            Catalogs = await _context.MtdCpqProposalCatalogs.Where(x => x.MtdCpqProposalId == MtdCpqProposal.Id).OrderBy(x => x.Sequence).ToListAsync();
            Items = await _context.MtdCpqProposalItem
                .Where(x => x.MtdCpqProposalId == MtdCpqProposal.Id && x.Selected == 1)
                .OrderBy(x => x.Sequence).ThenBy(x => x.Name)
                .ToListAsync();

            CatalogCulture = new CultureInfo(_config.Value.CatalogCulture, false);

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var oldProposal = await _context.MtdCpqProposal.FirstOrDefaultAsync(m => m.Id == MtdCpqProposal.Id);
            if (oldProposal == null) { return NotFound(); }

            /*Access check*/
            bool access = await _userHandler.CheckQuoteEditAsync(oldProposal.Id, HttpContext.User);
            if (!access) { return NotFound(); }

            WebAppUser user = await _userHandler.GetUserAsync(HttpContext.User);

            oldProposal.MasterPrice = MtdCpqProposal.MasterPrice;
            oldProposal.MasterQty = MtdCpqProposal.MasterQty;
            decimal total = MtdCpqProposal.MasterPrice * MtdCpqProposal.MasterQty;

            var oldItems = await _context.MtdCpqProposalItem.Where(x => x.MtdCpqProposalId == MtdCpqProposal.Id).ToListAsync();

            foreach (var item in oldItems)
            {
                var dataStr = Request.Form[$"{item.Id}-price"];
                var qtyStr = Request.Form[$"{item.Id}-qty"];


                bool isOk = decimal.TryParse(dataStr, out decimal dataDecimal);
                bool isOkInt = int.TryParse(qtyStr, out int qtyInt);
                if (!isOkInt || qtyInt < 1) { qtyInt = 1; }

                if (isOk)
                {
                    total += (dataDecimal * qtyInt);
                    item.Price = dataDecimal * qtyInt;
                    item.Qty = qtyInt;
                }
            }
         
            _context.UpdateRange(oldItems);
            _context.MtdCpqProposal.Update(oldProposal);
            await _context.SaveChangesAsync();
            
            await _userHandler.AddActionLogAsync(UserHandler.ActionType.EditPrice, User, oldProposal.Id);
            return RedirectToPage("./Details", new { id = oldProposal.Id });
        }

        public async Task<IActionResult> OnPostUpdateItemAsync(string id, bool isMaster = false)
        {
            if (id == null) { return new OkObjectResult("ERROR!"); }

            var qtyStr = Request.Form[$"{id}-qty"];
            var priceStr = Request.Form[$"{id}-price"];
            int qty = 1; decimal price = 0;
            bool isOk;
            isOk = int.TryParse(qtyStr, out int tempQty);
            if (isOk && tempQty != 0) { qty = tempQty; }
            isOk = decimal.TryParse(priceStr, out decimal tempPrice);
            if (isOk) { price = tempPrice; }

            MtdCpqProposal proposal;
            if (isMaster)
            {
                proposal = await _context.MtdCpqProposal.FindAsync(id);
                if (proposal == null ) { return new OkObjectResult("ERROR!"); }

                /*Access check*/
                bool access = await _userHandler.CheckQuoteEditAsync(proposal.Id, HttpContext.User);
                if (!access) { return NotFound(); }

                proposal.MasterPrice = price;
                proposal.MasterQty = qty;
                _context.MtdCpqProposal.Update(proposal);

            }
            else
            {
                var item = await _context.MtdCpqProposalItem.FindAsync(id);
                if (item == null) { return new OkObjectResult("ERROR!"); }
                proposal = await _context.MtdCpqProposal.FindAsync(item.MtdCpqProposalId);
                if (proposal == null) { return new OkObjectResult("ERROR!"); }

                /*Access check*/
                bool access = await _userHandler.CheckQuoteEditAsync(proposal.Id, HttpContext.User);
                if (!access) { return NotFound(); }

                item.Price = price;
                item.Qty = qty;
                _context.MtdCpqProposalItem.Update(item);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return new OkObjectResult("ERROR!");
            }


            await _userHandler.AddActionLogAsync(UserHandler.ActionType.EditPrice, User, proposal.Id);
            decimal sum = price * qty;
            return new OkObjectResult(sum.ToString("C2", CatalogCulture));
        }

    }
}
