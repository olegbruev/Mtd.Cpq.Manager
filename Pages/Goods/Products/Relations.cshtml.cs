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
    public class RelationsModel : PageModel
    {
        private readonly CpqContext _context;
        private readonly UserHandler userHandler;

        public RelationsModel(CpqContext context, UserHandler userHandler)
        {
            _context = context;
            this.userHandler = userHandler;
        }
     
        [BindProperty (Name = "product")]
        public MtdCpqProduct MtdCpqProduct { get; set; }       
        [BindProperty (Name = "catalog")]
        public MtdCpqCatalog Catalog { get; set; }
        public IList<MtdCpqCatalog> Catalogs { get; set; }
        [BindProperty]
        public string SearchText { get; set; }
        [BindProperty(Name ="cpage")]
        public IList<MtdCpqProduct> Products { get; set; }
        public IList<MtdCpqRuleAvailable> Rules { get; set; }

        public Paginator Paginator { get; set; }

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
        public List<MtdCpqOneOf> MtdCpqOneOfs { get; set; }
        public async Task<IActionResult> OnGetAsync(string product,
                string catalog, string searchText, string psearchText, bool archive=false, int pcpage=1, int cpage=1, string master = null)
        {
            if (product == null) { return NotFound(); }

            CPage = cpage;
            PCPage = pcpage;
            SearchText = searchText;
            Archive = archive;
            PSearchText = psearchText;
            Master = master;

            MtdCpqProduct = await _context.MtdCpqProduct
                .Include(m => m.MtdCpqCatalog).FirstOrDefaultAsync(m => m.Id == product);            

            if (MtdCpqProduct == null || MtdCpqProduct.Som == 0)
            {
                return NotFound();
            }
       

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
                .Where(x => x.MtdCpqCatalogId == Catalog.Id && x.Id != MtdCpqProduct.Id && x.Archive==0)
                .OrderBy(x => x.Sequence).ThenBy(x => x.Name);

            if (searchText != null)
            {
                string text = searchText.ToUpper().Replace(" ","");
                query = query.Where(x=>x.IdNumber.ToUpper().Replace(" ","").Contains(text) || x.Name.ToUpper().Replace(" ","").Contains(text));
            }

            int count = await query.CountAsync();
            Paginator = new Paginator(CPage, 25, count);

            if (count > Paginator.Size)
            {
                query = query.Skip(Paginator.Skip).Take(Paginator.Take);
            }

            Products = await query.ToListAsync();

            Rules = await _context.MtdCpqRuleAvailable.Where(x => x.ProductIdParent == MtdCpqProduct.Id).ToListAsync();

            MtdCpqOneOfs = await _context.MtdCpqOneOfs.OrderBy(x => x.Name).ToListAsync();
            MtdCpqOneOfs.Insert(0, new MtdCpqOneOf { Id = null, Name = "NO" });
          
            return Page();
        }

        public async Task<IActionResult> OnPostAvailablesAsync(string id, string product)
        {           
            var check = Request.Form[$"{id}-available-checkbox"];
            var rule = await _context.MtdCpqRuleAvailable.Where(x=>x.ProductIdParent == product && x.ProductIdChild == id).FirstOrDefaultAsync();
            if (rule == null && check == "true")
            {
                rule = new MtdCpqRuleAvailable {Id = Guid.NewGuid().ToString(), ProductIdParent = product, ProductIdChild = id };
                await _context.MtdCpqRuleAvailable.AddAsync(rule);
                await _context.SaveChangesAsync();
            }

            if (rule != null && check == "false")
            {                
                _context.MtdCpqRuleAvailable.Remove(rule);
                await _context.SaveChangesAsync();
            }

            await userHandler.AddActionLogAsync(UserHandler.ActionType.ProductRelationAvailables, HttpContext.User, product);
            return new OkResult();
        }

        public async Task<IActionResult> OnPostRequiredAsync(string id, string product)
        {
            var check = Request.Form[$"{id}-available-checkbox"];
            var rule = await _context.MtdCpqRuleAvailable.Where(x => x.ProductIdParent == product && x.ProductIdChild == id).FirstOrDefaultAsync();
            if (rule == null && check == "true")
            {
                rule = new MtdCpqRuleAvailable { Id = Guid.NewGuid().ToString(), ProductIdParent = product, ProductIdChild = id, Required = 1 };
                await _context.MtdCpqRuleAvailable.AddAsync(rule);
                await _context.SaveChangesAsync();
            }

            if (rule != null && check == "true")
            {
                rule.Required = 1;
                _context.MtdCpqRuleAvailable.Update(rule);
                await _context.SaveChangesAsync();
            }

            if (rule != null && check == "false")
            {
                rule.Required = 0;
                _context.MtdCpqRuleAvailable.Update(rule);
                await _context.SaveChangesAsync();
            }
            await userHandler.AddActionLogAsync(UserHandler.ActionType.ProductRelationRequired, HttpContext.User, product);
            return new OkResult();
        }

        public async Task<IActionResult> OnPostOneOfAsync(string id, string product)
        {
            string selectValue = Request.Form[$"{id}-oneof-select"];
            selectValue = selectValue == string.Empty ? null : selectValue;
            var rule = await _context.MtdCpqRuleAvailable.Where(x => x.ProductIdParent == product && x.ProductIdChild == id).FirstOrDefaultAsync();
            if (rule == null)
            {
                rule = new MtdCpqRuleAvailable { Id = Guid.NewGuid().ToString(), ProductIdParent = product, ProductIdChild = id, Required = 0, OneOfId = selectValue};
                await _context.MtdCpqRuleAvailable.AddAsync(rule);              
            } else
            {
                rule.OneOfId = selectValue;
                _context.MtdCpqRuleAvailable.Update(rule);
            }

            await _context.SaveChangesAsync();
            await userHandler.AddActionLogAsync(UserHandler.ActionType.ProductRelationOneOf, HttpContext.User, product);
            return new OkResult();
        }
    }
}
