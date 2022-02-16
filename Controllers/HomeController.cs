using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EZTFT.Models;
using EZTFT.ViewModels;
using EZTFT.Models.MatchModels;

namespace EZTFT.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {       
            return View();
        }
       
        public ActionResult About()
        {

            //WeatherModel weatherModel = new WeatherModel();

            //using (var client = new HttpClient())
            //{
            //    //client.BaseAddress = new Uri("http://api.weatherstack.com/current?access_key=8d8e628e00037d4a91d4e3a2d0551eca&query=37.8267,-122.4233&units=f");
            //    var url = new Uri("http://api.weatherstack.com/current?access_key=8d8e628e00037d4a91d4e3a2d0551eca&query=37.8267,-122.4233&units=f");
            //    //HTTP GET
            //    var responseTask = client.GetAsync(url);
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        var readTask = result.Content.ReadAsAsync<WeatherModel>();
            //        readTask.Wait();

            //        weatherModel = readTask.Result;
            //    }
            //    else //web api sent error response 
            //    {
            //        //log response status here..

            //        //weatherModel = Enumerable.Empty<WeatherModel>();

            //        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            //    }
            //}            

            //return View(weatherModel);
            return View();
        }

        public ActionResult Contact()
        {            

            return View();
        }
    }

   
}