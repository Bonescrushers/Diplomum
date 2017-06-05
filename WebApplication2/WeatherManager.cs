using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WebApplication2.Models;
namespace WebApplication2
{
    public class WeatherManager
    {
        private const string WeatherURL = "http://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&mode=json&cnt=4&units=metric&APPID=c28bb3a2fa9b9c8f6c4c73e707191bf2";
        private const string GeocodeURL = "https://maps.googleapis.com/maps/api/geocode/json?&latlng={0}&key=AIzaSyAj-_76uy1786zwWttsNHCZe310go7oTwM";

        public WeatherData GetForecast(Time time,Position position)
        {
            using (WebClient client = new WebClient())
            {
                string task = null;
                string Latitude = position.Latitude.ToString("0.1" + new string('#', 339), CultureInfo.InvariantCulture);
                string Longtitude = position.Longtitude.ToString("0.1" + new string('#', 339), CultureInfo.InvariantCulture);
                try
                {
                    string locationRequest= client.DownloadString(String.Format(GeocodeURL, Latitude+","+Longtitude));
                    dynamic locations = JsonConvert.DeserializeObject(locationRequest);
                    string location = locations.results[0].formatted_address;
                    task = client.DownloadString(String.Format(WeatherURL,Latitude,Longtitude));
                    dynamic forecast = JsonConvert.DeserializeObject(task);
                    var weather = new WeatherData();
                    weather.Location = location;
                    weather.Humidity = forecast.list[time.IntValue()].main.humidity;
                    weather.Pressure= forecast.list[time.IntValue()].main.pressure;
                    weather.Visibility = CalculateVisibility(weather.Humidity);
                    weather.WeatherPhenomena = forecast.list[time.IntValue()].weather[0].description;
                    weather.WindSpeed = forecast.list[time.IntValue()].wind.speed;
                    weather.WindDirection = forecast.list[time.IntValue()].wind.deg;
                    weather.Crosswind = CalculateCroswind(weather.WindDirection, 120.0, weather.WindSpeed);
                    weather.CloudBaseLevel = CalculateCloudBaseLevel(weather.Humidity);
                    return weather;
                }
                catch (HttpRequestException)
                {
                    throw;
                }
            }           
        }
        private double CalculateCloudBaseLevel(int humidity)
        {
            return 24 * (100 - humidity);
        }
        private double CalculateVisibility(int humidity)
        {
            return (320*(humidity - humidity)/320)+10;
        }

        private double CalculateCroswind(double directionWind,double directionFly,double speed)
        {
            double angle = Math.Abs(directionFly - directionWind);
            return Math.Sin(angle) * speed;
        }

        private double CalculateHeadwing(double directionWind, double directionFly, double speed)
        {
            double angle = Math.Abs(90-Math.Abs(directionFly - directionWind));
            return Math.Sin(angle) * speed;
        }
    }
}