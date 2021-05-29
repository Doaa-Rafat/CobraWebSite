using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CobraLocalization.Resources;
using CobraWebSite.Services;
using CobraWebSite.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CobraWebSite
{
    public class Startup
    {
        IList<CultureInfo> supportedCultures;

        RequestLocalizationOptions localizationOptions;

        private IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .Build();

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
           
            #region Culture and translation
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
            services.Configure<RequestLocalizationOptions>(options =>
                {
                    options.DefaultRequestCulture = localizationOptions.DefaultRequestCulture;
                    options.SupportedCultures = localizationOptions.SupportedCultures;
                    options.SupportedUICultures = localizationOptions.SupportedUICultures;

                    options.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider());
                });
            #endregion

            #region SettingsKeys
            SettingKeys settings = new SettingKeys();
            settings.CobraAPIURL = configuration["CobraAPIURL"];
            settings.DBConnectionString = configuration["ConnectionStrings:DefaultConnection"];
            
            ConfigurationManager.settingKeys = settings;
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, Services.MailService>();
            services.AddSingleton<ConfigurationManager>();
            #endregion
            services.AddMvc();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto});
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

                        mvcRoutes.MapRoute(
                            name: "productDetails",
                            template: "{culture=en-US}/Product/{id}",
                            defaults: new { Controller = "Product", Action = "ProductDetails" });

                        mvcRoutes.MapRoute(
                            name: "egyptian-granite",
                            template: "{culture=en-US}/egyptian-granite/{pageNumber?}",
                            defaults: new { Controller = "Product", Action = "ListProducts" , pageNumber = 1, MainCategoryId = 2 , CategoryType = 1 });

                        mvcRoutes.MapRoute(
                            name: "egyptian-marble",
                            template: "{culture=en-US}/egyptian-marble/{pageNumber?}",
                            defaults: new { Controller = "Product", Action = "ListProducts", pageNumber = 1, MainCategoryId = 1, CategoryType = 1 });
                        mvcRoutes.MapRoute(
                            name : "contact-us",
                            template: "{culture=en-US}/contact-us",
                            defaults: new
                            {
                                Controller = "Home",
                                Action = "ContactUs"
                            }
                            );
                        mvcRoutes.MapRoute(
                                name: "about-us",
                                template: "{culture=en-US}/about-us",
                                defaults: new
                                {
                                    Controller = "Home",
                                    Action = "AboutUs"
                                }
                                );
                    });
                });
            });
        }

    }
}
