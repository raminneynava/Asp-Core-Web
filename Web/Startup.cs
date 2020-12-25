using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Data.Interfaces;
using Entities;
using Data.Repositories;
using AutoMapper;
using Services.MappingProfiles;
using Microsoft.AspNetCore.Identity;
using Common.Identity;
using Services.MessageSender;

namespace Web
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddIdentity<AppUser, IdentityRole>(option =>
            {
                option.Password.RequireDigit = false;
                option.Password.RequiredUniqueChars = 0;
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.User.RequireUniqueEmail = true;
                option.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                option.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders().AddErrorDescriber<PersianIdentityErrorDescriber>();
            services.ConfigureApplicationCookie(opts =>
            {

                opts.LoginPath = "/Login";
                opts.AccessDeniedPath = "/Login";
                opts.ExpireTimeSpan = TimeSpan.FromHours(10);

            }
            );

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IMessageSender, MessageSender>();
            services.AddScoped(typeof(IBlogRepository), typeof(BlogRepository));
            services.AddAutoMapper(typeof(MappingProfile));
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

            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                  name: "Panel",
                  areaName: "Panel",
                  pattern: "Panel/{controller=Panel}/{action=Dashboard}");
                endpoints.MapAreaControllerRoute(
                  name: "User",
                  areaName: "User",
                  pattern: "User/{controller=User}/{action=Index}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
