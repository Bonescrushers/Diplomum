using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2
{
    public class ElevationManager
    {

        private const string GeocodeURL = "https://maps.googleapis.com/maps/api/elevation/json?path={0}&samples=100&key=AIzaSyAj-_76uy1786zwWttsNHCZe310go7oTwM";
        private const double kilometer = 0.00249527794;
        private const string DistanceUrl = "https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins={0}&destinations={1}&key=AIzaSyAj-_76uy1786zwWttsNHCZe310go7oTwM";

        public double CalculateMaxElevation(Position position)
        {
            using (WebClient client = new WebClient())
            {
                string json = null;
                try
                {
                    string startPosition = position.Latitude.ToString("0." + new string('#', 339),CultureInfo.InvariantCulture) + "," + position.Longtitude.ToString("0." + new string('#', 339), CultureInfo.InvariantCulture);
                    string lastPosition = (position.Latitude + kilometer).ToString("0." + new string('#', 339), CultureInfo.InvariantCulture) + "," + position.Longtitude.ToString("0." + new string('#', 339), CultureInfo.InvariantCulture);
                    json = client.DownloadString(String.Format(GeocodeURL, startPosition + "|" + lastPosition));
                    dynamic result = JsonConvert.DeserializeObject(json);
                    List<double> elevations = new List<double>();
                    foreach (var point in result.results)
                    {
                        var s = (double)point.elevation;
                        elevations.Add((double)point.elevation);
                    }
                    double difference = elevations.Max() - elevations.Min();
                    return difference;

                }
                catch (HttpRequestException)
                {
                    throw;
                }
            }
        }
    }
}