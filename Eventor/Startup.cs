using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eventor.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Eventor.Models;
using Eventor.Data.Entities;
using Eventor.Services;
using Eventor.Data.Repositories;
using DataAccess.Data.Interfaces;
using DataAccess.Data.Entities;
using DataAccess.Data.Repositories;
using AutoMapper;
using Services.Configuration;
using Services.Interfaces;
using Services;
using Services.ServiceConfiguration;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace Eventor
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.Zero;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
           
            services.AddOptions();
            services.Configure<MailConfig>(Configuration.GetSection("Mailing"));

            services.AddSingleton<IImageConfig, ImageConfig>
                (_ => new ImageConfig { WeebRoot = HostingEnvironment.WebRootPath });

            services.AddTransient<IRepository<Event>, EventRepository>();
            services.AddTransient<IRepository<Subscription>, SubscriptionRepository>();
            services.AddTransient<IRepository<ApplicationUser>, ApplicationUserRepository>();
            
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IEventService, EventService>();
            //services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<ISubscriptionService, SubscriptionService>();
            services.AddTransient<IApplicationUserService, ApplicationUserService>();
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            CreateRole(services.BuildServiceProvider()).Wait();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
          
        }

        public async Task CreateRole(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Organizer"};
            IdentityResult roleResult;


            foreach (var roleName in roleNames)
            {   
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var poweruser = new ApplicationUser
            {
                UserName = Configuration.GetSection("UserSettings")["UserEmail"],
                Email = Configuration.GetSection("UserSettings")["UserEmail"]
            };

            var user = await userManager.FindByEmailAsync("Eventor@gmail.com");

            if (user == null)
            {
                var createAdmin = await userManager.CreateAsync(poweruser, "Qwerty_12345");

                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(poweruser, "Admin");
                }

            }           
        }
    }
}
