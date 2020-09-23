using Microsoft.Extensions.Options;
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

        public ConfigHandler(IOptions<ConfigSettings> options)
        {
            this.options = options;
        }
    }
}
