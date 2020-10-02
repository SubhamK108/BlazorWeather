using System;

namespace Components
{
    class Location
    {
        public static bool IsLocationAvailable { get; private set; }
        public static string ApiKey { get; } = "IRXJXS7r3yAfZURYWHfsQOo8B8ECQFqP";
        public static string ErrorMessage { get; set; } = "Fetching your location";
        public static string Coords { get; set; } = "lat long";
        public static double Latitude { get; set; }
        public static double Longitude { get; set; }

        public static void ParseCoords()
        {
            if (Coords == "1 1")
            {
                IsLocationAvailable = false;
                ErrorMessage = "Please turn on your device's location services and refresh the app";
            }
            else
            {
                IsLocationAvailable = true;
                ErrorMessage = "Fetching weather data of your location";
            }

            Latitude = Convert.ToDouble(Coords.Split(" ")[0]);
            Longitude = Convert.ToDouble(Coords.Split(" ")[1]);
        }
    }
}