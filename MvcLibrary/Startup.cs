using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MvcLibrary.Models;
using MvcLibrary.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace MvcLibrary
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
            /*            services.AddMvc(options =>
                        {
                            var policy = new AuthorizationPolicyBuilder()
                                        .RequireAuthenticatedUser().Build();
                            options.Filters.Add(new AuthorizeFilter(policy));
                        }).AddXmlSerializerFormatters();
            */
            //services.AddSingleton<>();
            
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "209827349165-f7b51o3qrd7ljfi0e8b34mj6e456a875.apps.googleusercontent.com";
                    options.ClientSecret = "QzafopVCFYXTymVUBoeOr4oq";
                })
                .AddFacebook(options => 
                {
                    options.AppId = "500578444208094";
                    options.AppSecret = "7adb4f828a2040f5a79afff75763828a";
                });

            services.AddControllersWithViews();
            services.AddDbContext<MvcLibraryContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MvcLibraryContext")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            })
                .AddEntityFrameworkStores<MvcLibraryContext>()
                .AddDefaultTokenProviders()
//                .AddTokenProvider<CustomEmailConfirmmationTokenProvider<ApplicationUser>>("CustomEmailConfirmation")
                ;


            services.Configure<DataProtectionTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromMinutes(30));

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Administration/AccessDenied");
            });

            services.AddAuthorization(options =>
           {
               options.AddPolicy("EditRolePolicy", policy => policy.RequireClaim("Edit Role"));
               //               options.AddPolicy("DeleteRolePolicy", policy => policy.RequireClaim("Delete Role"));
               //               options.AddPolicy("EditRolePolicy", policy => policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));
               options.AddPolicy("AdminRolePolicy", policy => policy.RequireRole("Admin")); // "Admin,Hello q"
           });

 //           services.AddSingleton<IAuthorizationHandler, CanEditOnlyOtherAdminRolesAndClaimsHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Not Found URL =< !");
            });
        }
    }
}
/*
Add-Migration InitialCreate
Update-Database

Add-Migration Rating
Update-Database

Install-Package Microsoft.EntityFrameworkCore.SqlServer
 */