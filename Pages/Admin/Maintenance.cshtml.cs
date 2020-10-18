using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.AppConfig;
using Mtd.Cpq.Manager.Data;

namespace Mtd.Cpq.Manager.Pages.Admin
{

    public class Maintenance : PageModel
    {
        private readonly CpqContext context;

        public Maintenance(CpqContext context)
        {
            this.context = context;
        }

        [BindProperty]
        public string ScriptId { get; set; }
        
        [TempData]
        public string AlertMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["ScriptList"] = null;
            IList<MtdCpqConfig> configs = await context.MtdCpqConfig.Where(x => x.ValueType == ConfigType.AdminScript).OrderBy(x => x.Name).ToListAsync();
            if (configs.Count() > 0)
            {
                ViewData["ScriptList"] = new SelectList(configs, "Id", "Name");
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            string script = await context.MtdCpqConfig.Where(x => x.Id == ScriptId).Select(x => x.Value).FirstOrDefaultAsync();
            if (script == null) { return RedirectToPage("./Maintenance"); }
            await context.Database.BeginTransactionAsync();
            int rows;
            try
            {
                rows = await context.Database.ExecuteSqlRawAsync(script);
            }
            catch
            {
                context.Database.RollbackTransaction();
                throw new Exception("Error");
            }

            finally
            {
                context.Database.CommitTransaction();
            }

            AlertMessage =  $"Rows processed: {rows}";

            return RedirectToPage("./Maintenance");
        }

    }


    public class AlertMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
