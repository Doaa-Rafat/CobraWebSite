using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CobraWebSite.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult ListBlogs()
        {
            return View();
        }
        public IActionResult BlogDetails()
        {
            return View();
        }
    }
}
