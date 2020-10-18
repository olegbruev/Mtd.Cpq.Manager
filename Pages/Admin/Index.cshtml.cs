using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.AppConfig;
using Mtd.Cpq.Manager.Components;
using Mtd.Cpq.Manager.Data;

namespace Mtd.Cpq.Manager.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly CpqContext context;

        public byte[] LogoImg { get; set; }

        public IndexModel(CpqContext cpqContext)
        {
            context = cpqContext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            LogoImg = await context.MtdCpqConfigFiles.Where(x => x.Id == ConfigId.FileLogoImg).Select(x => x.FileData).FirstOrDefaultAsync();
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {

             bool exists = await  context.MtdCpqConfigFiles.Where(x=>x.Id == ConfigId.FileLogoImg).AnyAsync();
            
             MTDImgSModify imgSModify = await MTDImgSelector.ImageModifyAsync("LogoImg", Request);
            
            if (imgSModify.Modify && imgSModify.Image != null)
            {

                MtdCpqConfigFile cpqConfig = new MtdCpqConfigFile { Id = ConfigId.FileLogoImg,  FileName = imgSModify.Name,  FileSize = imgSModify.Size, FileType = imgSModify.ContentType, FileData  = imgSModify.Image};
                
                if (exists) { context.MtdCpqConfigFiles.Update(cpqConfig); } else { await context.MtdCpqConfigFiles.AddAsync(cpqConfig); }

                await context.SaveChangesAsync();
            }

            if (imgSModify.Image == null && imgSModify.Modify)
            {
                MtdCpqConfigFile cpqConfig = new MtdCpqConfigFile { Id = ConfigId.FileLogoImg };
                context.MtdCpqConfigFiles.Remove(cpqConfig);
                await context.SaveChangesAsync();
            }

                return RedirectToPage("./Index");
        }
    }
}
