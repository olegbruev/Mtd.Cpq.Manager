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

namespace Mtd.Cpq.Manager.Pages.Proposal
{
    public class ItemsCreateModel : PageModel
    {
        private readonly CpqContext _context;
        private readonly UserHandler _userHandler;

        public ItemsCreateModel(CpqContext context, UserHandler userHandler)
        {
            _context = context;
            _userHandler = userHandler;
        }

        [BindProperty(Name = "proposal")]
        public MtdCpqProposal MtdCpqProposal { get; set; }
        [BindProperty]
        public string SearchText { get; set; }
        [BindProperty(Name = "cpage")]
        public int CPage { get; set; }
        public Paginator Paginator { get; set; }

        public IList<MtdCpqProduct> Products { get; set; }
        public IList<MtdCpqCatalog> Catalogs { get; set; }

        [BindProperty(Name = "catalog")]
        public MtdCpqCatalog Catalog { get; set; }
        public async Task<IActionResult> OnGetAsync(string proposal, string searchText, int cpage = 1)
        {
            if (proposal == null) { return NotFound(); }
            CPage = cpage < 1 ? 1 : cpage;
            SearchText = searchText;

            MtdCpqProposal = await _context.MtdCpqProposal.FirstOrDefaultAsync(m => m.Id == proposal);
            if (MtdCpqProposal == null) { return NotFound(); }

            /*Access check*/
            bool access = await _userHandler.CheckQuoteEditAsync(MtdCpqProposal.Id, HttpContext.User);
            if (!access) { return NotFound(); }

            WebAppUser user = await _userHandler.GetUserAsync(HttpContext.User);

            IQueryable<string> masterIds = _context.MtdCpqRuleAvailable.GroupBy(x => x.ProductIdParent).Select(x => x.Key);

            IQueryable<MtdCpqProduct> query = _context.MtdCpqProduct
                  .Where(x => x.Som == 1 && x.Archive == 0 && masterIds.Contains(x.Id))
                  .OrderBy(x => x.MtdCpqCatalog.Sequence)
                  .ThenBy(x => x.Name);


            bool trialAccess = await _userHandler.CheckToTrialAccessAsync(User);
            if (trialAccess) { query = query.Where(x => x.Trial >= 0); } else { query = query.Where(x => x.Trial == 0); }


            if (searchText != null)
            {
                string text = searchText.ToUpper();
                query = query.Where(x => x.IdNumber.ToUpper().Contains(text) || x.Name.ToUpper().Contains(text));
            }

            int count = await query.CountAsync();
            Paginator = new Paginator(CPage, 10, count);

            if (count > Paginator.Size)
            {
                query = query.Skip(Paginator.Skip).Take(Paginator.Take);
            }

            Products = await query.ToListAsync();
            cpage = Paginator.CPage;

            return Page();
        }

        /// <summary>
        /// HttpPost Default
        /// </summary>
        /// <param name="proposal">Id Item</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(string product, string proposal)
        {
            if (product == null || proposal == null) { return NotFound(); }

            MtdCpqProposal = await _context.MtdCpqProposal
                .FirstOrDefaultAsync(m => m.Id == proposal);

            if (MtdCpqProposal == null) { return NotFound(); }

            /*Access check*/
            bool access = await _userHandler.CheckQuoteEditAsync(MtdCpqProposal.Id, HttpContext.User);
            if (!access) { return NotFound(); }

            WebAppUser user = await _userHandler.GetUserAsync(HttpContext.User);

            if (MtdCpqProposal.MasterId != null)
            {
                return RedirectToPage("./ItemsEdit", new { id = MtdCpqProposal.Id });
            }

            MtdCpqProduct dataProduct = await _context.MtdCpqProduct.Where(x => x.Id == product).FirstOrDefaultAsync();
            if (dataProduct == null) { return NotFound(); }

            MtdCpqProposal.MasterId = dataProduct.Id;
            MtdCpqProposal.MasterPrice = dataProduct.Price;
            MtdCpqProposal.MasterNumber = dataProduct.IdNumber;
            MtdCpqProposal.MasterName = dataProduct.Name;
            MtdCpqProposal.MasterNote = dataProduct.Note;
            MtdCpqProposal.MasterImage = dataProduct.Image;
            MtdCpqProposal.MasterDatasheet = dataProduct.Datasheet;
            MtdCpqProposal.MasterAfactor = dataProduct.Afactor;
            MtdCpqProposal.ViewAfactor = 1;

            _context.MtdCpqProposal.Update(MtdCpqProposal);

            var availables = await _context.MtdCpqRuleAvailable
                .Where(x => x.ProductIdParent == MtdCpqProposal.MasterId)
                .ToListAsync();

            var products = await _context.MtdCpqProduct.Where(x => availables.Select(s => s.ProductIdChild).Contains(x.Id) && x.Archive == 0).ToListAsync();

            var catalogs = await _context.MtdCpqCatalog.Where(x => products.Select(s => s.MtdCpqCatalogId).Contains(x.Id))
                .Select(x => new MtdCpqProposalCatalog
                {
                    Id = Guid.NewGuid().ToString(),
                    MtdCpqProposalId = MtdCpqProposal.Id,
                    CId = x.Id,
                    Name = x.Name,
                    IdNumber = x.IdNumber,
                    Image = x.Image,
                    Note = x.Note,
                    Sequence = x.Sequence
                }).ToListAsync();

            var oneOfIds = from p in availables.Where(x => x.OneOfId != null)
                           group p by p.OneOfId into g
                           select g.FirstOrDefault().OneOfId;

            var proposalOneOfs = await _context.MtdCpqOneOfs.Where(x => oneOfIds.Contains(x.Id)).Select(x => new MtdCpqProposalOneOf
            {
                Id = Guid.NewGuid().ToString(),
                MtdCpqProposalId = MtdCpqProposal.Id,
                CId = x.Id,
                Name = x.Name,
                Note = x.Note,
                Color = x.Color
            }).ToListAsync();


            List<MtdCpqProposalItem> items = new List<MtdCpqProposalItem>();
            List<MtdCpqProposalAnchor> anchors = new List<MtdCpqProposalAnchor>();

            foreach (var p in products)
            {
                string oneOfId = availables.Where(x => x.ProductIdChild == p.Id).Select(x => x.OneOfId).FirstOrDefault();
                if (oneOfId != null) { oneOfId = proposalOneOfs.Where(x => x.CId == oneOfId).Select(x => x.Id).FirstOrDefault(); }

                var anchor = await _context.MtdCpqRuleAnchor.Where(x => x.ProductMaster == MtdCpqProposal.MasterId && x.ProductAnchor == p.Id).FirstOrDefaultAsync();
                if (anchor == null)
                {
                    anchor = await _context.MtdCpqRuleAnchor
                        .Where(x => x.ProductMaster == null && x.ProductAnchor == p.Id).FirstOrDefaultAsync();
                }

                MtdCpqProposalItem item = new MtdCpqProposalItem
                {
                    Id = Guid.NewGuid().ToString(),
                    IdNumber = p.IdNumber,
                    Image = p.Image,
                    Name = p.Name,
                    Note = p.Note,
                    Datasheet = p.Datasheet,
                    MtdCpqProductId = p.Id,
                    MtdCpqProposalId = MtdCpqProposal.Id,
                    Required = availables.Where(a => a.ProductIdChild == p.Id).Select(a => a.Required).FirstOrDefault(),
                    Selected = availables.Where(a => a.ProductIdChild == p.Id).Select(a => a.Required).FirstOrDefault(),
                    Price = p.Price,
                    MtdCpqProposalOneOfId = oneOfId,
                    ProposalCatalogId = catalogs.Where(c => c.CId == p.MtdCpqCatalogId).Select(s => s.Id).FirstOrDefault(),
                    Sequence = p.Sequence
                };

                string notice = null;


                if (anchor != null)
                {

                    notice = anchor.Notice;
                    List<MtdCpqRuleAnchorBind> binds = await _context.MtdCpqRuleAnchorBind
                        .Where(x => x.MtdCpqRuleAnchorId == anchor.Id).ToListAsync();

                    binds.ForEach(bind =>
                    {
                        anchors.Add(new MtdCpqProposalAnchor
                        {
                            Id = Guid.NewGuid().ToString(),
                            Cid = item.Id,
                            MtdCpqProductId = bind.MtdCpqProductId,
                            Include = bind.Include,
                            Required = bind.Required
                        });
                    });

                };

                item.AnchorNotice = notice;
                items.Add(item);

            }

            //Set OneOf for first
            var datas = from p in items.Where(x => x.MtdCpqProposalOneOfId != null)
                        orderby p.Sequence
                        group p by p.MtdCpqProposalOneOfId into g
                        select new { g.FirstOrDefault().Id };

            foreach (var data in datas)
            {
                items.FirstOrDefault(x => x.Id == data.Id).Selected = 1;
            }

            await _context.MtdCpqProposalOneOf.AddRangeAsync(proposalOneOfs);
            await _context.MtdCpqProposalCatalogs.AddRangeAsync(catalogs);
            await _context.MtdCpqProposalItem.AddRangeAsync(items);
            await _context.MtdCpqProposalAnchor.AddRangeAsync(anchors);
            await _context.SaveChangesAsync();


            //Set Anchor Links
            ItemLink itemLink = new ItemLink(_context);
            itemLink.SetItems(items);
            itemLink.SetIAnchors(anchors);
            List<MtdCpqProposalItem> updateItems = new List<MtdCpqProposalItem>();
            foreach (var item in items)
            {
                if (item.Selected == 1)
                {
                    var tempItems = await itemLink.CheckAnchorsAsync(item.Id, MtdCpqProposal, 1);
                    updateItems.AddRange(tempItems);
                }
            }

            if (updateItems.Count > 0)
            {
                _context.MtdCpqProposalItem.UpdateRange(updateItems);
                await _context.SaveChangesAsync();
            }

            await _userHandler.AddActionLogAsync(UserHandler.ActionType.EditQuote, User, MtdCpqProposal.Id);
            return RedirectToPage("./ItemsEdit", new { id = MtdCpqProposal.Id });
        }

    }
}
