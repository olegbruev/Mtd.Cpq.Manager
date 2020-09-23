using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.DataConfig;
using Mtd.Cpq.Manager.Services;

namespace Mtd.Cpq.Manager.Pages.Proposal
{
    [Authorize (Roles = "cpq-admin")]
    public class EditOwnerModel : PageModel
    {
        private readonly CpqContext _context;
        private readonly UserHandler _userHandler;

        public EditOwnerModel(CpqContext context, UserHandler userHandler)
        {
            _context = context;
            _userHandler = userHandler;
        }

        [BindProperty]
        public MtdCpqProposal MtdCpqProposal { get; set; }
        public string CurrentOwner { get; set; }
        [BindProperty]
        public string NewOwner { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null) { return NotFound(); }
            MtdCpqProposal = await _context.MtdCpqProposal.FindAsync(id);
            if (MtdCpqProposal == null) { return NotFound(); }
            CurrentOwner = await _userHandler.GetQuoteOwnerNameAsync(id);            
            var userList = _userHandler.Users.OrderBy(x=>x.Title).Select(x=> new { x.Id, Title = $"{x.Title} ({x.TitleGroup})" }).ToList();

            ViewData["UserList"] = new SelectList(userList, "Id", "Title");
            return Page();
        }


        public async Task<IActionResult> OnPostSetOwnerAsync()
        {
            await _userHandler.SetQuoteOwnerAsync(MtdCpqProposal.Id, NewOwner, HttpContext.User);
            await _userHandler.AddActionLogAsync( UserHandler.ActionType.SetQuoteOwner,HttpContext.User,MtdCpqProposal.Id);
            return RedirectToPage("./EditOwner", new { id= MtdCpqProposal.Id });
        }
    }
}