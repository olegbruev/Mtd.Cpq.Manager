using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.Services;
using Mtd.OrderMaker.Server.Areas.Identity.Data;

namespace Mtd.Cpq.Manager.Pages.Proposal
{
    public class ItemsEditModel : PageModel
    {
        private readonly CpqContext _context;
        private readonly UserHandler _userHandler;

        public ItemsEditModel(CpqContext context, UserHandler userHandler)
        {
            _context = context;
            _userHandler = userHandler;

        }

        public MtdCpqProposal MtdCpqProposal { get; set; }

        public IList<MtdCpqProposalItem> Items { get; set; }
        public IList<MtdCpqProposalCatalog> Catalogs { get; set; }
        public IList<MtdCpqProposalOneOf> OneOfs { get; set; }

        [BindProperty]
        public string SearchText { get; set; }
        [BindProperty(Name = "catalog")]
        public MtdCpqProposalCatalog Catalog { get; set; }
        public async Task<IActionResult> OnGetAsync(string id, string searchText, string catalog)
        {
            if (id == null) { return NotFound(); }

            SearchText = searchText;
            MtdCpqProposal = await _context.MtdCpqProposal.FirstOrDefaultAsync(m => m.Id == id);
            if (MtdCpqProposal == null) { return NotFound(); }

            /*Access check*/
            bool access = await _userHandler.CheckQuoteEditAsync(MtdCpqProposal.Id, HttpContext.User);
            if (!access) { return NotFound(); }

            WebAppUser user = await _userHandler.GetUserAsync(HttpContext.User);

            Catalogs = await _context.MtdCpqProposalCatalogs
                .Where(x => x.MtdCpqProposalId == MtdCpqProposal.Id)
                .OrderBy(x => x.Sequence).ToListAsync();

            if (catalog == null) { Catalog = Catalogs.FirstOrDefault(); }
            else { Catalog = Catalogs.FirstOrDefault(x => x.Id == catalog); }

            if (Catalog == null) { return RedirectToPage("./ItemsCreate", new { proposal = MtdCpqProposal.Id }); }

            var query = _context.MtdCpqProposalItem
                .Where(x => x.MtdCpqProposalId == MtdCpqProposal.Id && x.ProposalCatalogId == Catalog.Id);

            if (SearchText != null)
            {
                string text = SearchText.ToUpper();
                query = query.Where(x => x.IdNumber.ToUpper().Contains(text) || x.Name.ToUpper().Contains(text));
            }

            Items = await query.OrderBy(x => x.Sequence).ThenBy(x => x.Name).ToListAsync();
            OneOfs = await _context.MtdCpqProposalOneOf.Where(x => x.MtdCpqProposalId == MtdCpqProposal.Id).ToListAsync();

            return Page();
        }

        public async Task<JsonResult> OnPostAsync(string id)
        {
            if (id == null) { return new JsonResult("NotFound"); }

            WebAppUser user = await _userHandler.GetUserAsync(HttpContext.User);

            var item = await _context.MtdCpqProposalItem.FirstOrDefaultAsync(x => x.Id == id);            
            if (item == null) { return new JsonResult("NotFound"); }
            var proposal = await _context.MtdCpqProposal.FindAsync(item.MtdCpqProposalId);
            if (proposal == null) { return new JsonResult("NotFound"); }

            /*Access check*/
            bool access = await _userHandler.CheckQuoteEditAsync(proposal.Id, HttpContext.User);
            if (!access) { return new JsonResult("NotFound"); }

            var result = Request.Form[$"{id}-checkbox"];
            sbyte selected = result == "true" ? (sbyte)1 : (sbyte)0;
           
            if (item.MtdCpqProposalOneOfId == null || proposal.ConfigChangeRule == 1)
            {
                item.Selected = selected;
                _context.MtdCpqProposalItem.Update(item);

            } else
            {
                selected = 1;
            }
            

            ItemLink itemLink = new ItemLink(_context);

            if (item.MtdCpqProposalOneOfId != null && proposal.ConfigChangeRule == 0)
            {
                item.Selected = 1;
                List<MtdCpqProposalItem> clearList = await _context.MtdCpqProposalItem
                    .Where(x => x.MtdCpqProposalId == item.MtdCpqProposalId && x.MtdCpqProposalOneOfId == item.MtdCpqProposalOneOfId && x.Id != item.Id)
                    .ToListAsync();
                foreach (MtdCpqProposalItem clearItem in clearList)
                {
                    clearItem.Selected = 0;
                    _context.MtdCpqProposalItem.Attach(clearItem);
                    _context.Entry(clearItem).Property(x => x.Selected).IsModified = true;            
                    List<MtdCpqProposalItem> clearItems = await itemLink.CheckAnchorsAsync(clearItem.Id, proposal, 0);
                    if (clearItems.Count > 0) { _context.MtdCpqProposalItem.UpdateRange(clearItems); }
                }
            }
            
            if (proposal.ConfigChangeRule == 0)
            {
                List<MtdCpqProposalItem> anchorItems = await itemLink.CheckAnchorsAsync(item.Id, proposal, selected);
                if (anchorItems.Count > 0) { _context.MtdCpqProposalItem.UpdateRange(anchorItems); }
            }

            await _context.SaveChangesAsync();
            
            IList<MtdCpqProposalOneOf> OneOfs = await _context.MtdCpqProposalOneOf
                .Where(one => one.MtdCpqProposalId == proposal.Id).ToListAsync();
            IList<MtdCpqProposalItem> Items = await _context.MtdCpqProposalItem
                .Where(x => x.MtdCpqProposalId == item.MtdCpqProposalId && x.ProposalCatalogId == item.ProposalCatalogId).ToListAsync();
                        
            var items = Items.GroupJoin(OneOfs, i => i.MtdCpqProposalOneOfId, o => o.Id, (i, o) => new
            {
                i.Id,
                i.Selected,
                i.Required,
                OneOfId = i.MtdCpqProposalOneOfId,
                Color = o.Select(x => x.Color).FirstOrDefault(),
                i.Forbidden
            }).ToList();

            if (proposal.ConfigChangeRule != 0) { 
                item.AnchorNotice = string.Empty; 
            }

            await _userHandler.AddActionLogAsync(UserHandler.ActionType.EditQuote, User, proposal.Id);
            return new JsonResult(new { proposal.ConfigChangeRule, items, item.AnchorNotice });
        }

        public async Task<IActionResult> OnPostDeleteAllAsync(string id)
        {

            if (id == null) { return NotFound(); }

            var MtdCpqProposal = await _context.MtdCpqProposal.FirstOrDefaultAsync(m => m.Id == id);
            if (MtdCpqProposal == null) { return NotFound(); }

            /*Access check*/
            bool access = await _userHandler.CheckQuoteEditAsync(MtdCpqProposal.Id, HttpContext.User);
            if (!access) { return new JsonResult("NotFound"); }

            WebAppUser user = await _userHandler.GetUserAsync(HttpContext.User);


            MtdCpqProposal.MasterPrice = 0;
            MtdCpqProposal.MasterId = null;
            MtdCpqProposal.MasterNumber = null;
            MtdCpqProposal.MasterName = null;
            MtdCpqProposal.MasterNote = null;
            MtdCpqProposal.MasterDatasheet = null;
            MtdCpqProposal.MasterImage = null;

            IList<MtdCpqProposalCatalog> dataDelet = await _context.MtdCpqProposalCatalogs
                .Where(x => x.MtdCpqProposalId == MtdCpqProposal.Id).Select(x => new MtdCpqProposalCatalog { Id = x.Id }).ToListAsync();
            _context.MtdCpqProposalCatalogs.RemoveRange(dataDelet);
            await _context.SaveChangesAsync();

            await _userHandler.AddActionLogAsync(UserHandler.ActionType.EditQuote, User, MtdCpqProposal.Id);
            return RedirectToPage("./ItemsCreate", new { proposal = MtdCpqProposal.Id });
        }

        public async Task<JsonResult> OnPostIncludeAsync(string id)
        {
            if (id == null) { return new JsonResult("NotFound"); }
            var proposal = await _context.MtdCpqProposal.FindAsync(id);
            if (proposal == null) { return new JsonResult("NotFound"); }

            /*Access check*/
            bool access = await _userHandler.CheckQuoteEditAsync(proposal.Id, HttpContext.User);
            if (!access) { return new JsonResult("NotFound"); }

            proposal.ConfigMasterInluded = proposal.ConfigMasterInluded == 1 ? (sbyte)0 : (sbyte)1;
            _context.MtdCpqProposal.Update(proposal);
            await _context.SaveChangesAsync();
            
            await _userHandler.AddActionLogAsync(UserHandler.ActionType.EditQuote, User, proposal.Id);
            return new JsonResult(proposal.ConfigMasterInluded);
        }

        public async Task<JsonResult> OnPostRuleAsync(string id, string catalog)
        {
            if (id == null) { return new JsonResult("NotFound"); }
            var proposal = await _context.MtdCpqProposal.FindAsync(id);
            if (proposal == null) { return new JsonResult("NotFound"); }

            /*Access check*/
            bool access = await _userHandler.CheckQuoteEditAsync(proposal.Id, HttpContext.User);
            if (!access) { return new JsonResult("NotFound"); }

            WebAppUser user = await _userHandler.GetUserAsync(HttpContext.User);

            proposal.ConfigChangeRule = proposal.ConfigChangeRule == 1 ? (sbyte)0 : (sbyte)1;
            _context.MtdCpqProposal.Update(proposal);

            List<MtdCpqProposalItem> allItems = await _context.MtdCpqProposalItem
                .Where(x => x.MtdCpqProposalId == proposal.Id).ToListAsync();

            if (proposal.ConfigChangeRule == 0)
            {
                //All required fields selecting
                allItems.Where(x => x.Required == 1).ToList().ForEach(a => { a.Selected = 1; });

                //Clearing the items that keep the "OR OR" rule
                allItems.Where(x => x.MtdCpqProposalOneOfId != null).ToList().ForEach(a => a.Selected = 0);

                //List of Ids belonging to the "OR OR" rule
                var groupOneOf = from p in allItems.Where(x => x.MtdCpqProposalOneOfId != null)
                                 orderby p.IdNumber
                                 group p by p.MtdCpqProposalOneOfId into g
                                 select new { ItemId = g.FirstOrDefault().Id, OneOfId = g.FirstOrDefault().MtdCpqProposalOneOfId };

                //Update of fields that are in the top list of the "One Of" rule
                allItems.Where(x => groupOneOf.Select(x => x.ItemId).Contains(x.Id)).ToList().ForEach(f => f.Selected = 1);

            }
            else
            {
                allItems.Where(x => x.Selected == 1).ToList().ForEach(a => a.Selected = 0);
            }


            await _context.SaveChangesAsync();

            IList<MtdCpqProposalOneOf> OneOfs = await _context.MtdCpqProposalOneOf
                .Where(one => one.MtdCpqProposalId == proposal.Id).ToListAsync();
            IList<MtdCpqProposalItem> Items = await _context.MtdCpqProposalItem
                .Where(x => x.MtdCpqProposalId == proposal.Id && x.ProposalCatalogId == catalog).ToListAsync();

            var items = Items.GroupJoin(OneOfs, i => i.MtdCpqProposalOneOfId, o => o.Id, (i, o) => new
            {
                i.Id,
                i.Selected,
                i.Required,
                OneOfId = i.MtdCpqProposalOneOfId,
                Color = o.Select(x => x.Color).FirstOrDefault()
            }).ToList();
            
            await _userHandler.AddActionLogAsync(UserHandler.ActionType.EditQuote, User, proposal.Id);

            return new JsonResult(new { proposal.ConfigChangeRule, items });
        }
        
    }
}