using System;
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
using Mtd.OrderMaker.Server.Areas.Identity.Data;

namespace Mtd.Cpq.Manager.Pages.Proposal
{
    public class EditModel : PageModel
    {
        private readonly CpqContext _context;
        private readonly UserHandler _userHandler;
        private readonly IOptions<ConfigSettings> _config;

        public EditModel(CpqContext context, UserHandler userHandler, IOptions<ConfigSettings> config)
        {
            _context = context;
            _userHandler = userHandler;
            _config = config;
        }

        [BindProperty]
        public MtdCpqProposal MtdCpqProposal { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null) {
                return NotFound();
            }
            
            MtdCpqProposal = await _context.MtdCpqProposal.FirstOrDefaultAsync(m => m.Id == id);
            if (MtdCpqProposal == null)  { return NotFound();  }

            /*Access*/
            bool access = await _userHandler.CheckQuoteEditAsync(MtdCpqProposal.Id, HttpContext.User);
            if (!access) { return NotFound(); }
            
            Language language = new Language();
            string lang = MtdCpqProposal.Language;
            
            if(lang == null)
            {
                lang= _config.Value.CultureInfo;
                MtdCpqProposal.Language = lang;
            }            

            ViewData["Language"] = new SelectList(language.Items, "Culture", "Name", language.Items.Where(x => x.Culture.Equals(lang)));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            MtdCpqProposal proposal = await _context.MtdCpqProposal.AsNoTracking().FirstOrDefaultAsync(m => m.Id == MtdCpqProposal.Id);

            if (proposal == null) { return NotFound(); }

            WebAppUser user = await _userHandler.GetUserAsync(HttpContext.User);

            /*Access check*/
            bool access = await _userHandler.CheckQuoteEditAsync(proposal.Id, HttpContext.User);
            if (!access) { return NotFound(); }

            _context.Attach(proposal).State = EntityState.Modified;

            MTDImgSModify imgSModify = await MTDImgSelector.ImageModifyAsync(MtdCpqProposal.Id, Request);
            proposal.Logo = imgSModify.Image;
            _context.Entry(proposal).Property(x => x.Logo).IsModified = imgSModify.Modify;
                        
            _context.Entry(proposal).Property(x => x.DateCreation).IsModified = false;
            
            proposal.ContactName = MtdCpqProposal.ContactName;
            proposal.ContactPhone = MtdCpqProposal.ContactPhone;
            proposal.ContactEmail = MtdCpqProposal.ContactEmail;
            proposal.PreparedBy = MtdCpqProposal.PreparedBy;
            proposal.TitleName = MtdCpqProposal.TitleName;
            proposal.TitleNote = MtdCpqProposal.TitleNote;
            proposal.PreparedFor = MtdCpqProposal.PreparedFor;
            proposal.Description = MtdCpqProposal.Description;
            proposal.Language = MtdCpqProposal.Language;
            proposal.LogoWidth = proposal.LogoWidth <= 0 ? 250 : proposal.LogoWidth;
            proposal.LogoHeight = proposal.LogoHeight <= 0 ? 100 : proposal.LogoHeight;

            try
            {
                await _context.SaveChangesAsync();
                await _userHandler.AddActionLogAsync(UserHandler.ActionType.EditQuote, User, proposal.Id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MtdCpqProposalExists(MtdCpqProposal.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Details", new { id = MtdCpqProposal.Id});
        }


        private bool MtdCpqProposalExists(string id)
        {
            return _context.MtdCpqProposal.Any(e => e.Id == id);
        }
    }
}
