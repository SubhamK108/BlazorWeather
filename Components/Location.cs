using System;
using static Components.WeatherFetcher;

namespace Components
{
    class Location
    {
        public static string ApiKey { get; } = "3Bvx6DuChwG7JQ0PE22SUPWxKJkGo2Hc";
        public static string ErrorMessage { get; set; } = "Fetching your location";
        public static string Coords { get; set; } = "lat long";
        public static double Latitude { get; set; }
        public static double Longitude { get; set; }

        public static void ParseCoords()
        {
            if (Coords == "1 1")
            {
                ErrorMessage = "Please turn on your device's location services and refresh the app";
                EndProcessWithError();
            }
            else
            {
                ErrorMessage = "Fetching weather data of your location";
                Latitude = Convert.ToDouble(Coords.Split(" ")[0]);
                Longitude = Convert.ToDouble(Coords.Split(" ")[1]);
            }
        }
    }
}