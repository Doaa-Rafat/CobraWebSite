using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CobraLocalization.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CobraWebSite
{
    public class Startup
    {
        IList<CultureInfo> supportedCultures;

        RequestLocalizationOptions localizationOptions;
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<LocService>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc()
        .AddViewLocalization()
        .AddDataAnnotationsLocalization(options =>
        {
            options.DataAnnotationLocalizerProvider = (type, factory) =>
            {
                var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                return factory.Create("SharedResource", assemblyName.Name);
            };
        });
            supportedCultures = new List<CultureInfo>
                        {
                    new CultureInfo("ar-EG"),
                    new CultureInfo("en-US"),
                        };

            localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "ar-EG", uiCulture: "ar-EG"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures


            };
            localizationOptions.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider());
            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    options.DefaultRequestCulture = localizationOptions.DefaultRequestCulture;
                    options.SupportedCultures = localizationOptions.SupportedCultures;
                    options.SupportedUICultures = localizationOptions.SupportedUICultures;

                    options.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider());
                });

            services.AddMvc();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouter(routes =>
            {
                routes.MapMiddlewareRoute("{culture=en-US}/{*mvcRoute}", subApp =>
                {
                    subApp.UseRequestLocalization(localizationOptions);

                    subApp.UseMvc(mvcRoutes =>
                    {
                        mvcRoutes.MapRoute(
                            name: "default",
                            template: "{culture=en-US}/{controller=Home}/{action=Index}/{id?}");
                    });
                });
            });
        }

    }
}
