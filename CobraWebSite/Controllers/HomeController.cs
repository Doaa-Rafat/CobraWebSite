using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CobraWebSite.DB;
using CobraWebSite.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CobraWebSite.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            #region Get Main Categories from API 
            var mainCategories =await ProductQueries.GetMainGategories();
            #endregion
            return View(mainCategories);
        }
    }
}