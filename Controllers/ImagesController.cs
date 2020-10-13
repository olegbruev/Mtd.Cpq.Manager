using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.AppConfig;
using Mtd.Cpq.Manager.Data;

namespace Mtd.Cpq.Manager.Controllers
{
    [Route("images")]
    [ApiController]
    [AllowAnonymous]
    public class ImagesController : ControllerBase
    {
        public readonly CpqContext context;
        public readonly IWebHostEnvironment env;

        public ImagesController(CpqContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }

        [HttpGet("logo.png")]
        public async Task<IActionResult> OnGetLogoAsync()
        {
            byte[] LogoImg = null;

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
            } else
            {
                string root = env.ContentRootPath;
                LogoImg = System.IO.File.ReadAllBytes($"{root}/wwwroot/img/logo-title.png");
            }
            
            return new FileContentResult(LogoImg, "image/png"); 
        }

    }
}
