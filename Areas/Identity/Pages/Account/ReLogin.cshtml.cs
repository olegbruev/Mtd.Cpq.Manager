using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mtd.Cpq.Manager.Extensions;
using Mtd.OrderMaker.Server.Areas.Identity.Data;

namespace Mtd.Cpq.Manager.Areas.Identity.Pages.Account
{
    public class ReLoginModel : PageModel
    {
        private readonly UserManager<WebAppUser> userManager;

        public ReLoginModel(UserManager<WebAppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            WebAppUser user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null) { return RedirectToPage("Identity/Account/Login"); }

            await userManager.RemoveClaimAsync(user, new Claim("revoke", "false"));
            await HttpContext.RefreshLoginAsync();
        
           return RedirectToPage("/Index");
        }
    }
}
