using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mtd.Cpq.Manager.Components;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.DataConfig;
using Mtd.Cpq.Manager.Models;
using Mtd.Cpq.Manager.Services;

using static Mtd.Cpq.Manager.Services.UserHandler;

namespace Mtd.Cpq.Manager.Pages.Supervision.Parameters
{
    public class EditModel : PageModel
    {
        private readonly CpqContext _context;
        private readonly UserHandler _userHandler;
        private readonly IOptions<ConfigSettings> _config;

        public EditModel(Mtd.Cpq.Manager.Data.CpqContext context, UserHandler userHandler, IOptions<ConfigSettings> config)
        {
            _context = context;
            _userHandler = userHandler;
            _config = config;
        }

        [BindProperty]
        public MtdCpqTitles MtdCpqTitles { get; set; }
        public bool IsEditor { get; set; }

        [BindProperty]
        public bool LogoFlexible { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null) { return NotFound(); }


            MtdCpqTitles = await _context.MtdCpqTitles.FindAsync(id);
            if (MtdCpqTitles == null) { return NotFound(); }

            /*Access check*/
            bool access = await _userHandler.CheckTitlesAccessAsync(id, HttpContext.User);
            if (!access) { return NotFound(); }

            IsEditor = await _userHandler.CheckTitlesEditAsync(id, HttpContext.User);

            Language language = new Language();
            string lang = MtdCpqTitles.Language;

            if (lang == null)
            {
                lang = _config.Value.CultureInfo;
                MtdCpqTitles.Language = lang;
            }

            LogoFlexible = MtdCpqTitles.LogoFlexible == 1;

            ViewData["Language"] = new SelectList(language.Items, "Culture", "Name", language.Items.Where(x => x.Culture.Equals(lang)));

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool access = await _userHandler.CheckTitlesEditAsync(MtdCpqTitles.Id, HttpContext.User);
            if (!access) { return RedirectToPage("./Index"); }


            _context.MtdCpqTitles.Attach(MtdCpqTitles).State = EntityState.Modified;

            MTDImgSModify imgSModify = await MTDImgSelector.ImageModifyAsync(MtdCpqTitles.Id, Request);
            MtdCpqTitles.Logo = imgSModify.Image;
            _context.Entry(MtdCpqTitles).Property(x => x.Logo).IsModified = imgSModify.Modify;

            if (MtdCpqTitles.LogoWidth <= 0) { MtdCpqTitles.LogoWidth = 250; }
            if (MtdCpqTitles.LogoHeight <= 0) { MtdCpqTitles.LogoHeight = 100; }
            MtdCpqTitles.LogoFlexible = LogoFlexible ? (sbyte)1 : (sbyte)0;

            await _context.SaveChangesAsync();
            await _userHandler.AddActionLogAsync(ActionType.EditTitles, HttpContext.User, MtdCpqTitles.Id);


            return RedirectToPage("./Index");
        }


        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var form = await HttpContext.Request.ReadFormAsync();
            string id = form["delete-input"];

            MtdCpqTitles titles = _context.MtdCpqTitles.Where(x => x.Id == id).FirstOrDefault();
            if (titles == null) { return NotFound(); }

            /*Access check*/
            bool access = await _userHandler.CheckTitlesEditAsync(id, HttpContext.User);
            if (!access) { return NotFound(); }

            _context.MtdCpqTitles.Remove(titles);
            await _context.SaveChangesAsync();
            await _userHandler.RemoveTitlesOwnerAsync(titles.Id, HttpContext.User);
            await _userHandler.AddActionLogAsync(UserHandler.ActionType.RemoveTitles, HttpContext.User, titles.Id);

            return RedirectToPage("./Index");
        }

    }
}
