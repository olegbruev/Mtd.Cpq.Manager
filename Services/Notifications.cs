using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mtd.Cpq.Manager.Services
{

    public class Notifications
    {
        private readonly CpqContext context;

        public Notifications (CpqContext context)
        {
            this.context = context;
        }

        public async Task<int> GetQtyForUserAsync(ClaimsPrincipal user)
        {
            int fact = await context.MtdCpqNotifications.CountAsync();
            int readed = await context.MtdCpqReaderUsers.Where(x => x.UserName == user.Identity.Name).CountAsync();
            return fact - readed;
        }

    }
}
