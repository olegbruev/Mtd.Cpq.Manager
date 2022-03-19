using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using Mtd.Cpq.Manager.DataConfig;
using Mtd.Cpq.Manager.Services;
using Microsoft.AspNetCore.DataProtection;
using System.IO;
using Microsoft.Extensions.Hosting;
using Mtd.OrderMaker.Server.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using Mtd.OrderMaker.Server.Entity;


namespace Mtd.Cpq.Manager
{
    public class Startup
    {

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }


        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
                options.Secure = CookieSecurePolicy.Always;
            });

            services.ConfigureApplicationCookie(options => {
                options.Cookie.Domain = Configuration.GetConnectionString("Domain");
                options.Cookie.Name = ".MTD.Service";      
            });

            services.AddDataProtection()
                .SetApplicationName($"{Configuration.GetConnectionString("ClientName")}")
                .PersistKeysToFileSystem(new DirectoryInfo(Configuration.GetConnectionString("KeysFolder")));

            services.AddDbContext<IdentityDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("IdentityConnection"), new MySqlServerVersion(new Version(8, 0))));

            services.AddDbContext<CpqContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DataConnection"), new MySqlServerVersion(new Version(8, 0))));

            services.AddDefaultIdentity<WebAppUser>(config =>
            {
                config.SignIn.RequireConfirmedEmail = false;
                config.User.RequireUniqueEmail = true;

            })
            .AddRoles<WebAppRole>()
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RoleAdmin", policy => policy.RequireRole(SystemRoles.Admin));
                options.AddPolicy("RoleGoodsManager", policy => policy.RequireRole(SystemRoles.Admin, SystemRoles.GoodsManager));
                options.AddPolicy("RoleSupervisor", policy => policy.RequireRole(SystemRoles.Admin, SystemRoles.GoodsManager, SystemRoles.Supervisor));                
                options.AddPolicy("RoleSalesManager", policy => policy.RequireRole(SystemRoles.Admin, SystemRoles.GoodsManager, SystemRoles.Supervisor, SystemRoles.SalesManager));
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    factory.Create(typeof(SharedResource));
                })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/");
                    options.Conventions.AuthorizeFolder("/Admin", "RoleAdmin");
                    options.Conventions.AuthorizeFolder("/Supervision", "RoleSupervisor");
                    options.Conventions.AuthorizeFolder("/Goods", "RoleGoodsManager");
                    options.Conventions.AuthorizeFolder("/Proposal", "RoleSalesManager");
                });

            services.AddTransient<IEmailSenderBlank, EmailSenderBlank>();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.Configure<PersonalMenu>(Configuration.GetSection("PersonalMenu"));

            services.AddScoped<UserHandler>();
            services.AddScoped<Notifications>();
            services.Configure<ConfigSettings>(Configuration.GetSection("ConfigSettings"));
            services.AddScoped<ConfigHandler>();


            IMvcBuilder builder = services.AddRazorPages();
            #if DEBUG
            if (Environment.IsDevelopment())
            {
                builder.AddRazorRuntimeCompilation();
            }
            #endif

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = false;
            });

            services.AddHostedService<MigrationService>();
        }
       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();         
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();        
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

        }
    }
}
