using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mtd.Cpq.Manager.Data;
using Mtd.Cpq.Manager.DataConfig;
using Mtd.OrderMaker.Server.Areas.Identity.Data;
using Mtd.OrderMaker.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mtd.Cpq.Manager
{
    public class DataProcessing
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IApplicationBuilder _app;

        public DataProcessing(IServiceProvider serviceProvider, IApplicationBuilder app) {
            _serviceProvider = serviceProvider;
            _app = app;
        }


        private void UpdateData()
        {
            using var serviceScope = _app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<CpqContext>();
            context.Database.Migrate();

            var contextIdentity = serviceScope.ServiceProvider.GetService<IdentityDbContext>();
            contextIdentity.Database.Migrate();
        }

        public void StartAsync()
        {

            var config = _serviceProvider.GetRequiredService<IOptions<ConfigSettings>>();
            
            if (config.Value.DataUpdate == "true") { UpdateData(); }
            if (config.Value.DataInit != "true") { return; }

            var roleEditor = _serviceProvider.GetRequiredService<RoleManager<WebAppRole>>();
            Task<bool> hasAdminRole = roleEditor.RoleExistsAsync("cpq-admin");
            hasAdminRole.Wait();

            if (!hasAdminRole.Result)
            {
                var roleAdmin = new WebAppRole
                {
                    Name = SystemRoles.Admin,
                    NormalizedName = SystemRoles.Admin.ToUpper(),
                    Title = "Administrator",
                    Seq = 50
                };

                var roleGoodsManager = new WebAppRole
                {
                    Name = SystemRoles.GoodsManager,
                    NormalizedName = SystemRoles.GoodsManager.ToUpper(),
                    Title = "Goods manager",
                    Seq = 40
                };

                var roleSupervisor = new WebAppRole
                {                    
                    Name = SystemRoles.Supervisor,
                    NormalizedName = SystemRoles.Supervisor.ToUpper(),
                    Title = "Supervisor",
                    Seq = 30
                };

                var roleSalesManager = new WebAppRole
                {
                    Name = SystemRoles.SalesManager,
                    NormalizedName = SystemRoles.SalesManager.ToUpper(),
                    Title = "Sales manager",
                    Seq = 20
                };

                var roleGuest = new WebAppRole
                {
                    Name = SystemRoles.Guest,
                    NormalizedName = SystemRoles.Guest.ToUpper(),
                    Title = "Guest",
                    Seq = 10
                };

                roleEditor.CreateAsync(roleAdmin).Wait();
                roleEditor.CreateAsync(roleSupervisor).Wait();
                roleEditor.CreateAsync(roleGoodsManager).Wait();
                roleEditor.CreateAsync(roleSalesManager).Wait();
                roleEditor.CreateAsync(roleGuest).Wait();
            }

            var userManager = _serviceProvider.GetRequiredService<UserManager<WebAppUser>>();
            Task<bool> hasUser = userManager.Users.AnyAsync();
            hasUser.Wait();

            if (!hasUser.Result)
            {

                config = _serviceProvider.GetRequiredService<IOptions<ConfigSettings>>();

                WebAppUser webAppUser = new WebAppUser
                {
                    Email = config.Value.EmailSupport,
                    EmailConfirmed = true,
                    Title = config.Value.DefaultUSR,
                    UserName = config.Value.EmailSupport,

                };

                userManager.CreateAsync(webAppUser, config.Value.DefaultPWD).Wait();
                userManager.AddToRoleAsync(webAppUser, SystemRoles.Admin).Wait();

            }           

        }
    }
}
