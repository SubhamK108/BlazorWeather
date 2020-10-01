using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http;
using static Components.Location;
using static Components.Weather;

namespace Components
{
    class City
    {
        public string Key { get; set; }
        public string EnglishName { get; set; }
        public Dictionary<string, object> AdministrativeArea { get; set; }

        public static string CityID { get; private set; }
        public static string Town { get; private set; }
        public static string State { get; private set; }

        private static City Deserialize(string jsonString)
        {
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true
            };

            return JsonSerializer.Deserialize<City>(jsonString);
        }

        public static async void GetCityDetails()
        {
            using (var httpClient = new HttpClient())
            {
                string cityURL = $"https://dataservice.accuweather.com/locations/v1/cities/geoposition/search?apikey={API}&q={Latitude},{Longitude}&toplevel=false";
                var response = await httpClient.GetAsync(cityURL);
                response.EnsureSuccessStatusCode();
                string responseString = await response.Content.ReadAsStringAsync();

                try
                {
                    City city = Deserialize(responseString);

                    CityID = city.Key;
                    Town = city.EnglishName;
                    State = Convert.ToString(city.AdministrativeArea["EnglishName"]);

                    GetWeatherDetails();
                }
                catch (System.Exception)
                {
                    if (!IsLocationON)
                        ErrorMessage = "Please turn on your device's location services and refresh the app";
                    else
                        ErrorMessage = "There was a network problem, kindly refresh the app. If the problem persists, then that means the daily limit of API Calls has been exhausted, so then please wait till Subham renews the API";
                }
            }
        }
    }
}