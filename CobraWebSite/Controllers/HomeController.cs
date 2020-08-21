using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CobraWebSite.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CobraWebSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            #region Get Main Categories from API 

            var mainCategories = APIUtilities.GetMainCategories();
            #endregion
            return View(mainCategories);
        }
    }
}