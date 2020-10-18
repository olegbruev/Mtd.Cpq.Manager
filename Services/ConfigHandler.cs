using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mtd.Cpq.Manager.AppConfig;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.DataConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mtd.Cpq.Manager.Services
{
    public class ConfigHandler
    {
        public readonly IOptions<ConfigSettings> options;
        private readonly CpqContext context;
        private readonly IWebHostEnvironment env;

        public ConfigHandler(CpqContext context, IOptions<ConfigSettings> options, IWebHostEnvironment env)
        {
            this.options = options;
            this.context = context;
            this.env = env;
        }


        public async Task<string> GetLogo()
        {
            string result = string.Empty;

            MtdCpqConfigFile LogoImg = await context.MtdCpqConfigFiles.Where(x => x.Id == ConfigId.FileLogoImg).FirstOrDefaultAsync();
            if (LogoImg != null && LogoImg.FileData != null)
            {
                string base64 = Convert.ToBase64String(LogoImg.FileData);
                result = string.Format("data:{0};base64,{1}", LogoImg.FileType, base64);
            } else
            {
                string root = env.ContentRootPath;
                byte[] fileData = System.IO.File.ReadAllBytes($"{root}/wwwroot/img/logo-title.png");
                string base64 = Convert.ToBase64String(fileData);
                result = string.Format("data:{0};base64,{1}", "image/png", base64);
            }

            return result;
        }
    }
}
