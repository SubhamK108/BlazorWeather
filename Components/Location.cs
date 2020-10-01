using System;

namespace Components
{
    class Location
    {
        public static bool IsLocationON { get; private set; }
        public static string API { get; } = "41lLMbZLvlLpHzlmw4GJ9G8iActG7U0U";
        public static string ErrorMessage { get; set; } = "Please turn on your device's location services and refresh the app";
        public static string Coords { get; set; } = "lat long";
        public static double Latitude { get; set; }
        public static double Longitude { get; set; }

        public static void ParseCoords()
        {
            if (Coords == "0 0")
            {
                IsLocationON = false;
            }
            else
            {
                IsLocationON = true;
                ErrorMessage = "Fetching Weather Data";
            }

            Latitude = Convert.ToDouble(Coords.Split(" ")[0]);
            Longitude = Convert.ToDouble(Coords.Split(" ")[1]);
        }
    }
}