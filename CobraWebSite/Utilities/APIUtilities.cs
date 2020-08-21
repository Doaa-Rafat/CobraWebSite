using CobraWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CobraWebSite.Utilities
{
    public class APIUtilities
    {
        public static List<MainCategory> GetMainCategories()
        {
            List<MainCategory> mainCategories = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.settingKeys.CobraAPIURL);
                //HTTP GET
                var responseTask = client.GetAsync("allmaincategory");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<MainCategory>>();
                    readTask.Wait();

                    mainCategories = readTask.Result;
                    return mainCategories;
                }
            }
            return null;
        }

        /*
         /*
             IEnumerable<StudentViewModel> students = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:64189/api/");
            //HTTP GET
            var responseTask = client.GetAsync("student");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<StudentViewModel>>();
                readTask.Wait();

                students = readTask.Result;
            }
            else //web api sent error response 
            {
                //log response status here..

                students = Enumerable.Empty<StudentViewModel>();

                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
        }
        return View(students);
         */
    }
}
