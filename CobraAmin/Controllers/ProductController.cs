using CobraAmin.DB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobraAmin.Controllers
{
    public class ProductController : Controller
    {
        public async Task<IActionResult> ListProducts(int MainCategoryId, int CategoryType, int pageNumber = 1)
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
            var products = await ProductQueries.ListProducts(pageNumber, PageSize, MainCategoryId, CategoryType);
            #endregion
            ViewBag.ImageFolderName = SubImageFolder.ToString();
            return View(products);
        }
    }
}
