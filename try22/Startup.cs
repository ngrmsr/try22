using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using try22.Models;
using Microsoft.AspNetCore.Identity;
using try22.Inferastructure;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace try22
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DemoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IUserValidator<AppUser>, CustomValidator>();
            services.AddIdentity<AppUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequiredLength = 3;
                o.Password.RequiredUniqueChars = 2;
                o.Password.RequireLowercase = false;
                o.Password.RequireNonAlphanumeric = false;

            })
                .AddEntityFrameworkStores<DemoContext>()
                .AddErrorDescriber<CustomIdentityError>()
                .AddDefaultTokenProviders();
            //services.ConfigureApplicationCookie(c => { c.LoginPath = ""; });
            services.AddSingleton<IClaimsTransformation, ClaimsProvider>();
            services.AddAuthorization(p =>
            {
                p.AddPolicy("for female", options =>
{
    options.RequireClaim(ClaimTypes.Gender, "female");


});
                p.AddPolicy("just female", options =>
                {
                    options.AddRequirements(new OnleFemaleRequerment() { Name="nmesri"});


                });
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
