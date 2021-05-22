using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<IActionResult> ListProducts(int MainCategoryId, int CategoryType,int pageNumber = 1)
        {
            int PageSize = 10;

            StringBuilder SubImageFolder = CategoryType == 1 ? new StringBuilder("egyptian-") : new StringBuilder("imported-");
            switch (MainCategoryId)
            {
                case 2:
                    SubImageFolder.Append("granite");
                    break;
                case 1:
                    SubImageFolder.Append("marble");
                    break;
                default:
                    SubImageFolder.Append("stone");
                    break;
            }


            #region Get List Of products from API 
            var products = await ProductQueries.ListProducts(pageNumber,PageSize, MainCategoryId , CategoryType);
            #endregion
            ViewBag.ImageFolderName = SubImageFolder.ToString();
            return View(products);
        }

        public IActionResult ProductDetails(string id)
        {
            #region Get details Of a product  by id from API 
            var productDetails = ProductQueries.GetProductDetails(id,"en");
            #endregion
            if (productDetails != null)
                return View(productDetails);
            else
                return View("~/Views/Shared/404.cshtml");
        }
    }
}