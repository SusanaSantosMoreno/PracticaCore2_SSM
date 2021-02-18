using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PracticaCore2_SSM.Data;
using PracticaCore2_SSM.Helpers;
using PracticaCore2_SSM.Repositories;

namespace MvcCore{
    public class Startup{

        IConfiguration configuration;

        public Startup(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services){
            services.AddTransient<LibrosRepository>();
            services.AddDbContext<LibrosContext>(options => options.
                UseSqlServer(this.configuration.GetConnectionString("DdbbExamen")));

            /*TEMPDATA*/
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();

            /*AUTHENTICATION*/
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie();

            /*CACHING*/
            services.AddSingleton<CachingService>();
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddResponseCaching();

            /*SESSION*/
            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();
            services.AddSession();


            services.AddControllersWithViews(options => options.EnableEndpointRouting = false).
                AddSessionStateTempDataProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseResponseCaching();
            app.UseSession();
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}"
                );
            });
        }
    }
}
