using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CobraWebSite.DB;
using CobraWebSite.Models;
using CobraWebSite.Services;
using CobraWebSite.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CobraWebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly MailSettings _mailSettings;

        private readonly IMailService mailService;
        public HomeController(IMailService mailService, IOptions<MailSettings> mailSettings)
        {
            this.mailService = mailService;
            _mailSettings = mailSettings.Value;

        }
        public async Task<IActionResult> Index()
        {
            #region Get Main Categories from API 
            var mainCategories =await ProductQueries.GetMainGategories();
            #endregion
            return View(mainCategories);
        }
        public IActionResult AboutUS()
        {
            
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ContactUs([FromForm]Contact model)
        {
            try
            {
                MailRequest mailRequest = new MailRequest {FromEmail = _mailSettings.Mail, ToEmail = model.Email, UserName = string.Concat(model.FirstName, ' ', model.LastName)  , Body = model.Message , Subject = "contact us"};
                await mailService.SendEmailAsync(mailRequest);
                //return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}