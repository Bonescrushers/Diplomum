using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class WeatherData
    {
        public string Location { get; set; }
        public double Pressure { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double WindDirection { get; set; }
        public double Crosswind { get; set; }
        public string WeatherPhenomena { get; set; }
        public double Visibility { get; set; }
        public double CloudBaseLevel { get; set; }
    }

    public class Position
    {
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
    }
}