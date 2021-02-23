using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mtd.Cpq.Manager.Components;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.Models;
using Mtd.Cpq.Manager.Services;
using Mtd.OrderMaker.Server.Areas.Identity.Data;
using static Mtd.Cpq.Manager.Services.UserHandler;

namespace Mtd.Cpq.Manager.Pages.Supervision.Parameters
{
    public class CreateModel : PageModel
    {
        private readonly CpqContext _context;
        private readonly UserHandler _userHandler;

        public CreateModel(CpqContext context, UserHandler userHandler)
        {
            _context = context;
            _userHandler = userHandler;
        }
        
        public IActionResult OnGetAsync()
        {
            Language language = new Language();
            ViewData["Language"] = new SelectList(language.Items, "Culture", "Name", language.Items.Where(x => x.Culture.Equals("en-US")));            
            return Page();
        }

        [BindProperty]
        public MtdCpqTitles MtdCpqTitles { get; set; }
        
        [BindProperty]
        public bool LogoFlexible { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            MTDImgSModify imgSModify = await MTDImgSelector.ImageModifyAsync("1", Request);
            MtdCpqTitles.Logo = imgSModify.Image;
            MtdCpqTitles.LogoWidth = MtdCpqTitles.LogoWidth <=0 ? 250 : MtdCpqTitles.LogoWidth;
            MtdCpqTitles.LogoHeight = MtdCpqTitles.LogoHeight <= 0 ? 100 : MtdCpqTitles.LogoHeight;
            MtdCpqTitles.LogoFlexible = LogoFlexible ? (sbyte) 1 : (sbyte) 0;

            MtdCpqTitles.Id = Guid.NewGuid().ToString();
            WebAppUser appUser = await _userHandler.GetUserAsync(HttpContext.User);
            bool isOk = await _userHandler.SetTitlesOwnerAsync(MtdCpqTitles.Id, appUser.Id, Request.HttpContext.User);
            if (isOk)
            {
                _context.MtdCpqTitles.Add(MtdCpqTitles);
                await _context.SaveChangesAsync();
                await _userHandler.AddActionLogAsync(ActionType.CreateTitles,HttpContext.User, MtdCpqTitles.Id);
            }           

            return RedirectToPage("./Index");
        }


    }
    
}