using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.DataConfig;
using Mtd.Cpq.Manager.Models;
using Mtd.Cpq.Manager.Services;

namespace Mtd.Cpq.Manager.Pages.Goods.Products
{
    public class IndexModel : PageModel
    {
        private readonly CpqContext _context;
        private readonly IOptions<ConfigSettings> _config;
        private readonly UserHandler userHandler;

        public IndexModel(CpqContext context, IOptions<ConfigSettings> config, UserHandler userHandler)
        {
            _context = context;
            _config = config;
            this.userHandler = userHandler;
        }
      
        [BindProperty]
        public IList<MtdCpqProduct> MtdCpqProducts { get;set; }
        [BindProperty]
        public string SearchText { get; set; }
        [BindProperty(Name = "catalog")]
        public MtdCpqCatalog Catalog { get; set; }
        public IList<MtdCpqCatalog> Catalogs { get; set; }

        public IList<MtdCpqRuleAnchor> Anchors { get; set; }
        public IList<MtdCpqRuleAnchorBind> AnchorBinds { get; set; }

        public Paginator Paginator { get; set; }       
        [BindProperty(Name = "cpage")]
        public int CPage { get; set; }
        [BindProperty(Name = "master")]
        public string Master { get; set; }
        public CultureInfo CultureInfo { get; set; }
        [BindProperty]
        public bool Archive { get; set; }
        public async Task<IActionResult> OnGetAsync(string searchText, string catalog, int cpage=1, 
                bool archive = false, string master = null)
        {
            CPage = cpage < 1 ? 1 : cpage;           
            SearchText = searchText;
            Archive = archive;
            Catalogs = await _context.MtdCpqCatalog.OrderBy(x => x.Sequence).ToListAsync();
            Master = master;

            var masterList = await _context.MtdCpqProduct.Where(x => x.Som == 1).OrderBy(x=>x.Name)
                .Select(x=> new {x.Id, Name = $"{x.IdNumber} {x.Name}" } ).ToListAsync();

            masterList.Insert(0, new { Id = string.Empty, Name = "All products" });

            ViewData["MasterList"] = new SelectList(masterList, "Id", "Name", master ?? string.Empty);

            if (catalog == null)
            {
                Catalog = Catalogs.FirstOrDefault();
            }
            else
            {
                Catalog = Catalogs.FirstOrDefault(x => x.Id == catalog);
            }

            if (Catalog == null) { return RedirectToPage("/Goods/Catalog/Index"); }


            IQueryable<MtdCpqProduct> query = _context.MtdCpqProduct
                .Where(x => x.MtdCpqCatalogId == Catalog.Id)
                .Include(m => m.MtdCpqCatalog)
                .OrderBy(x => x.Sequence).ThenBy(x => x.Name);

            if (master != null)
            {
                IList<string> childIds = await _context.MtdCpqRuleAvailable
                    .Where(x => x.ProductIdParent == master).Select(x=>x.ProductIdChild).ToListAsync();
                childIds.Add(master);
                query = query.Where(x => childIds.Contains(x.Id));
            }
            
            if (!archive)
            {
                query = query.Where(x => x.Archive==0);
            }

            if (searchText != null)
            {
                string text = searchText.ToUpper().Replace(" ", string.Empty);
                query = query.Where(x => x.IdNumber.ToUpper().Contains(text) || x.Name.ToUpper().Replace(" ", string.Empty).Contains(text));   
            }

            int count = await query.CountAsync();
            Paginator = new Paginator(CPage, 10, count);
            if (count > Paginator.Size)
            {
                query = query.Skip(Paginator.Skip).Take(Paginator.Take);
            }

            MtdCpqProducts = await query.ToListAsync();
            IList<string> idsProduct = MtdCpqProducts.Select(x => x.Id).ToList();
            Anchors = await _context.MtdCpqRuleAnchor.Where(x => idsProduct.Contains(x.ProductAnchor) && x.ProductMaster == master).ToListAsync();
            IList<string> idsAnchor = Anchors.Select(x => x.Id).ToList();
            AnchorBinds = await _context.MtdCpqRuleAnchorBind.Where(x => idsAnchor.Contains(x.MtdCpqRuleAnchorId)).ToListAsync();

            CultureInfo = new CultureInfo(_config.Value.CultureInfo, false);
            cpage = Paginator.CPage;


            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            string ID = string.Empty;
            await Task.Run(() => ID = Catalog.Id);
            return RedirectToPage("./Index", new { searchText = SearchText, page = CPage, catalog = Catalog.Id });
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            string id = Request.Form["delete-input"];
            MtdCpqProduct product = new MtdCpqProduct { Id = id };
            _context.MtdCpqProduct.Remove(product);
            await _context.SaveChangesAsync();
            await userHandler.AddActionLogAsync(UserHandler.ActionType.ProductDelete, HttpContext.User, id);

            return RedirectToPage("./Index", new { searchText = SearchText, catalog=Catalog.Id });
        }
    }
}
