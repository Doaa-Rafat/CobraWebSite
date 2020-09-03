using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CobraWebSite.DB;
using Microsoft.AspNetCore.Mvc;

namespace CobraWebSite.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Action to List any type of product based on MainCategoryId and CategoryType
        /// </summary>
        /// <param name="MainCategoryId">Marble = 1,Granite  = 2, Stone =3 </param>
        /// <param name="CategoryType">Egyptian = 1 , Imported = 2</param>
        /// <returns></returns>
        public async Task<IActionResult> ListProducts(int MainCategoryId, int CategoryType)
        {

            #region Get List Of products from API 
            var products = await ProductQueries.ListProducts(MainCategoryId , CategoryType);
            #endregion
            return View(products);
        }
        public IActionResult ProductDetails(int id)
        {

            return View();
        }
    }
}