using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.Models;
using Mtd.Cpq.Manager.Services;

namespace Mtd.Cpq.Manager.Pages.Goods.Products
{
    public class AnchorModel : PageModel
    {
        private readonly CpqContext _context;
        private readonly UserHandler userHandler;

        public AnchorModel(CpqContext context, UserHandler userHandler)
        {
            _context = context;
            this.userHandler = userHandler;
        }

        [BindProperty(Name = "product")]
        public MtdCpqProduct MtdCpqProduct { get; set; }
        [BindProperty(Name = "catalog")]
        public MtdCpqCatalog Catalog { get; set; }
        public IList<MtdCpqCatalog> Catalogs { get; set; }
        [BindProperty]
        public string SearchText { get; set; }
        [BindProperty(Name = "cpage")]
        public IList<MtdCpqProduct> Products { get; set; }
        public IList<MtdCpqRuleAnchorBind> Binds { get; set; }

        public Paginator Paginator { get; set; }

        [BindProperty]
        public bool StartOnMaster { get; set; }
        [BindProperty]
        public int CPage { get; set; }
        [BindProperty]
        public int PCPage { get; set; }
        [BindProperty]
        public string PSearchText { get; set; }
        [BindProperty]
        public bool Archive { get; set; }
        [BindProperty(Name = "master")]
        public string Master { get; set; }
        public string AnchorID { get; set; }
        public List<MtdCpqOneOf> MtdCpqOneOfs { get; set; }
        public string Notice { get; set; }
        public async Task<IActionResult> OnGetAsync(string product,
                string catalog, string searchText, string psearchText, bool archive = false, int pcpage = 1, int cpage = 1, string master = null)
        {
            if (product == null) { return NotFound(); }
            CPage = cpage;
            PCPage = pcpage;
            SearchText = searchText;
            Archive = archive;
            PSearchText = psearchText;
            Master = master;

            var masterList = await _context.MtdCpqProduct.Where(x => x.Som == 1).OrderBy(x => x.Name)
                .Select(x => new { x.Id, Name = $"{x.IdNumber} {x.Name}" }).ToListAsync();
            masterList.Insert(0, new { Id = string.Empty, Name = "All products" });
            ViewData["MasterList"] = new SelectList(masterList, "Id", "Name", master ?? string.Empty);


            MtdCpqProduct = await _context.MtdCpqProduct
                .Include(m => m.MtdCpqCatalog).FirstOrDefaultAsync(m => m.Id == product);

            if (MtdCpqProduct == null)
            {
                return NotFound();
            }

            StartOnMaster = MtdCpqProduct.Som == 0 ? false : true;

            Catalogs = await _context.MtdCpqCatalog.OrderBy(x => x.Sequence).ToListAsync();
            if (catalog == null)
            {
                Catalog = Catalogs.FirstOrDefault();
            }
            else
            {
                Catalog = Catalogs.FirstOrDefault(x => x.Id == catalog);
            }

            if (Catalog == null) { return NotFound(); }

            IQueryable<MtdCpqProduct> query = _context.MtdCpqProduct
                .Where(x => x.MtdCpqCatalogId == Catalog.Id && x.Id != MtdCpqProduct.Id && x.Archive == 0)
                .OrderBy(x => x.Sequence).ThenBy(x => x.Name);

            if (master != null)
            {
                IList<string> childIds = await _context.MtdCpqRuleAvailable
                    .Where(x => x.ProductIdParent == master).Select(x => x.ProductIdChild).ToListAsync();
                childIds.Add(master);
                query = query.Where(x => childIds.Contains(x.Id));
            }

            if (searchText != null)
            {
                string text = searchText.ToUpper().Replace(" ", "");
                query = query.Where(x => x.IdNumber.ToUpper().Replace(" ", "").Contains(text) || x.Name.ToUpper().Replace(" ", "").Contains(text));
            }

            int count = await query.CountAsync();
            Paginator = new Paginator(CPage, 25, count);

            if (count > Paginator.Size)
            {
                query = query.Skip(Paginator.Skip).Take(Paginator.Take);
            }

            Products = await query.ToListAsync();
            Binds = new List<MtdCpqRuleAnchorBind>();

            var anchor = await _context.MtdCpqRuleAnchor
                 .Where(x => x.ProductMaster == Master && x.ProductAnchor == MtdCpqProduct.Id)
                 .FirstOrDefaultAsync();

            if (anchor != null)
            {
                Notice = anchor.Notice;
                AnchorID = anchor.Id;
                Binds = await _context.MtdCpqRuleAnchorBind.Where(x => x.MtdCpqRuleAnchorId == anchor.Id).ToListAsync();
            }

            MtdCpqOneOfs = await _context.MtdCpqOneOfs.OrderBy(x => x.Name).ToListAsync();
            MtdCpqOneOfs.Insert(0, new MtdCpqOneOf { Id = null, Name = "NO" });

            return Page();
        }

        public async Task<IActionResult> OnPostSetLinkAsync(string product_master, string product_parent, string product_child, string actionType)
        {
            var check = Request.Form[$"{product_child}-{actionType}-checkbox"];

            var anchor = await _context.MtdCpqRuleAnchor
                 .Where(x => x.ProductMaster == product_master && x.ProductAnchor == product_parent)
                 .FirstOrDefaultAsync();

            if (anchor == null)
            {
                anchor = new MtdCpqRuleAnchor
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductMaster = product_master,
                    ProductAnchor = product_parent
                };

                await _context.MtdCpqRuleAnchor.AddAsync(anchor);
                await _context.SaveChangesAsync();
            }

            var bind = await _context.MtdCpqRuleAnchorBind
                .Where(x => x.MtdCpqRuleAnchorId == anchor.Id && x.MtdCpqProductId == product_child).FirstOrDefaultAsync();

            if (bind != null && actionType == "nolink")
            {
                _context.MtdCpqRuleAnchorBind.Remove(bind);
            }

            if (bind != null && actionType != "nolink")
            {
                switch (actionType)
                {
                    case "include":
                        {
                            bind.Include = check == "true" ? (sbyte)1 : (sbyte)0;
                            break;
                        }
                    case "exclude":
                        {
                            bind.Include = check == "true" ? (sbyte)0 : (sbyte)1;
                            break;
                        }

                    case "required":
                        {
                            bind.Required = check == "true" ? (sbyte)1 : (sbyte)0;
                            break;
                        }
                }

                _context.MtdCpqRuleAnchorBind.Update(bind);
            }

            if (bind == null && actionType != "nolink")
            {
                bind = new MtdCpqRuleAnchorBind { Id = Guid.NewGuid().ToString(), MtdCpqRuleAnchorId = anchor.Id, MtdCpqProductId = product_child };


                switch (actionType)
                {
                    case "include":
                        {
                            bind.Include = check == "true" ? (sbyte)1 : (sbyte)0;
                            break;
                        }
                    case "exclude":
                        {
                            bind.Include = check == "true" ? (sbyte)0 : (sbyte)1;
                            break;
                        }

                    case "required":
                        {
                            bind.Include = check == "true" ? (sbyte)1 : (sbyte)0;
                            bind.Required = check == "true" ? (sbyte)1 : (sbyte)0; 
                            break;
                        }
                }

                await _context.MtdCpqRuleAnchorBind.AddAsync(bind);

            }

            await _context.SaveChangesAsync();
            await userHandler.AddActionLogAsync(UserHandler.ActionType.ProductAnchorLink, HttpContext.User, anchor.ProductMaster);

            return new OkResult();
        }

        public async Task<IActionResult> OnPostSaveNoticeAsync(string master_id, string product_id)
        {
            string notice = Request.Form["text-notice-writer"];

            MtdCpqRuleAnchor anchor = await _context.MtdCpqRuleAnchor
                .Where(x => x.ProductMaster == master_id && x.ProductAnchor == product_id).FirstOrDefaultAsync();

            if (anchor == null)
            {
                anchor = new MtdCpqRuleAnchor
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductAnchor = product_id,
                    ProductMaster = master_id,
                    Notice = notice
                };

                await _context.MtdCpqRuleAnchor.AddAsync(anchor);
            }
            else
            {
                anchor.Notice = notice;
                _context.MtdCpqRuleAnchor.Update(anchor);
            }

            await _context.SaveChangesAsync();
            await userHandler.AddActionLogAsync(UserHandler.ActionType.ProductAnchorNotice, HttpContext.User, anchor.ProductMaster);
            return new OkResult();
        }



    }
}
