using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProMVC.WebFramework;
using ProMVC.WebFramework.DistributedCache;
using ProMVC.WebFramework.Middlewares;
using ProMVC.WebFramework.ModelBinder;
using ProMVC.WebFramework.Models;
using System;

namespace ProMVC.UI.WebApplication
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<ConnectionString>(Configuration.GetSection("ConnectionStrings"));

            services.Configure<IPListConfiguration>(Configuration.GetSection("IPListConfiguration"));

            services.AddSingleton<IIPListConfiguration>(
                resolver => resolver.GetRequiredService<IOptions<IPListConfiguration>>().Value);


            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            //••••••••••••••••Cache•••••••••••••••••••••••
            services.AddRedisDistributedCache();
            services.AddSQLServerDistributedCache();
            services.AddMemoryCache();


            services.AddTransient<ICacheProvider>(c =>
            {
                bool DistributedCacheIsEnabled =
                Convert.ToBoolean(Configuration.GetSection("InUseInUseDistributedCache").Value);

                if (DistributedCacheIsEnabled)
                {
                    return new DistributedCacheProvider(c.GetRequiredService<IDistributedCache>());
                }
                else
                {
                    return new IMemoryCacheProvider(c.GetRequiredService<IMemoryCache>());
                }
            });

            //••••••••••••••••Cache•••••••••••••••••••••••





            services.AddMvc(options =>
            {
                options.UsePersianizeStringModelBinder();
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseCustomExceptionHandlerMiddleware();
                //app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

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
