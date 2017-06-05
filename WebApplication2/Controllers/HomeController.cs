using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {          
            return View();
        }
        [HttpPost]
        public ActionResult Index(InputData Data)
        {
            WeatherManager weatherManager = new WeatherManager();
            ElevationManager elevationManager = new ElevationManager();
            Position position = new Position { Latitude = 1.0, Longtitude = 1.0 };
            WeatherData weatherData = weatherManager.GetForecast(Time.H6, position);
            double difference = elevationManager.CalculateMaxElevation(position);
            Results result = new Results();
            result.Location = weatherData.Location;
            result.Visibility = weatherData.Visibility.ToString() + " Km";
            result.Weather = weatherData.WeatherPhenomena;
            result.Relief = difference.ToString()+" m";
            result.Pressure = weatherData.Pressure.ToString() + " Hpa";
            return PartialView("ResultForm",result);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}