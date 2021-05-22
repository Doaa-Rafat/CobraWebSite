using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CobraLocalization.Resources;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using CobraAmin.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Routing;

namespace CobraAmin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        IList<CultureInfo> supportedCultures;

        RequestLocalizationOptions localizationOptions;

        private IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .Build();
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<LocService>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddControllersWithViews();
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

            services.AddSingleton<ConfigurationManager>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto });
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

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

            app.UseAuthorization();

            app.UseEndpoints(routes =>
            {
                routes.MapControllerRoute(name: "default",
                            pattern: "/",
                            defaults: new { Controller = "Home", Action = "Index"});

                routes.MapControllerRoute(
                    name: "egyptiangranite",
                    pattern: "egyptian/granite/{pageNumber}",
                    defaults: new { Controller = "Product", Action = "ListProducts", pageNumber = 1, MainCategoryId = 2, CategoryType = 1 });

                routes.MapControllerRoute(
                    name: "importedgranite",
                    pattern: "imported/granite/{pageNumber}",
                    defaults: new { Controller = "Product", Action = "ListProducts", pageNumber = 1, MainCategoryId = 2, CategoryType = 2 });

                routes.MapControllerRoute(
                    name: "egyptianmarble",
                    pattern: "egyptian/marble/{pageNumber}",
                    defaults: new { Controller = "Product", Action = "ListProducts", pageNumber = 1, MainCategoryId = 1, CategoryType = 1 });

                routes.MapControllerRoute(
                    name: "importedmarble",
                    pattern: "imported/marble/{pageNumber}",
                    defaults: new { Controller = "Product", Action = "ListProducts", pageNumber = 1, MainCategoryId = 1, CategoryType = 2 });

                
                 routes.MapControllerRoute(
                            name: "productDetails",
                            pattern: "Product/{id}",
                            defaults: new { Controller = "Product", Action = "EditProductDetails" });
                 

                //routes.MapControllerRoute(subApp =>
                //{
                //    subApp.UseRequestLocalization(localizationOptions);

                //    subApp.UseEndpoints(mvcRoutes =>
                //    {
                //        mvcRoutes.MapControllerRoute(
                //            );

                //        mvcRoutes.MapControllerRoute(
                //            name: "productDetails",
                //            pattern: "{culture=en-US}/ProductDetails/{id}",
                //            defaults: new { Controller = "Product", Action = "ProductDetails" });

                //        mvcRoutes.MapControllerRoute(
                //            name: "egyptian-granite",
                //            pattern: "{culture=en-US}/egyptian-granite/{pageNumber?}",
                //            defaults: new { Controller = "Product", Action = "ListProducts" , pageNumber = 1, MainCategoryId = 2 , CategoryType = 1 });

                //        mvcRoutes.MapControllerRoute(
                //            name: "egyptian-marble",
                //            pattern: "{culture=en-US}/egyptian-marble/{pageNumber?}",
                //            defaults: new { Controller = "Product", Action = "ListProducts", pageNumber = 1, MainCategoryId = 1, CategoryType = 1 });

                //    });
                //});
            });
        }
    }
}
