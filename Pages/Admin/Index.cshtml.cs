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
            string logoImg = await context.MtdCpqConfig.Where(x => x.Id == ConfigId.LogoImg).Select(x => x.Value).FirstOrDefaultAsync();
            if (logoImg != null)
            {
                int NumberChars = logoImg.Length;
                byte[] bytes = new byte[NumberChars / 2];
                for (int i = 0; i < NumberChars; i += 2)
                {
                    bytes[i / 2] = Convert.ToByte(logoImg.Substring(i, 2), 16);
                }

                LogoImg = bytes;
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {

             bool exists = await  context.MtdCpqConfig.Where(x=>x.Id == ConfigId.LogoImg).AnyAsync();
            
             MTDImgSModify imgSModify = await MTDImgSelector.ImageModifyAsync("LogoImg", Request);
            
            if (imgSModify.Modify && imgSModify.Image != null)
            {
                string logoImg = BitConverter.ToString(imgSModify.Image).Replace("-", "");
                
                MtdCpqConfig cpqConfig = new MtdCpqConfig { Id = ConfigId.LogoImg, Name = ConfigId.LogoImg.ToString(), Value = logoImg };
                
                if (exists) { context.MtdCpqConfig.Update(cpqConfig); } else { await context.MtdCpqConfig.AddAsync(cpqConfig); }

                await context.SaveChangesAsync();
            }

            if (imgSModify.Image == null && exists)
            {
                MtdCpqConfig cpqConfig = new MtdCpqConfig { Id = ConfigId.LogoImg };
                context.MtdCpqConfig.Remove(cpqConfig);
                await context.SaveChangesAsync();
            }

                return RedirectToPage("./Index");
        }
    }
}
