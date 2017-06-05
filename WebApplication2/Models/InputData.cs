using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class InputData
    {
        public string Name { get; set; }
        public AviatorRank AviatorRank { get; set; }
        public AircrafClass AircrafClass { get; set; }
        public double Wind { get; set; }
        public Time Time { get; set; }
    }

    public enum Time
    {
        Now,
        H3,
        H6,
        H9
    }

    public static class ExtensionMethods
    {
        public static int IntValue(this Enum argEnum)
        {
            return Convert.ToInt32(argEnum);
        }
    }
    public class Results
    {
        public string Location { get; set; }
        public string Weather { get; set; }
        public string Relief { get; set; }
        public string Pressure { get; set; }
        public string Visibility { get; set; }
    }

    public enum AircrafClass
    {
        A,
        B,
        C,
        D
    }

    public enum AviatorRank
    {
        I,
        II,
        III
    }


}