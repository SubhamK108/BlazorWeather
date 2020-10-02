using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http;
using static Components.Location;
using static Components.CityFetcher;

namespace Components
{
    class WeatherFetcher
    {
        public static int Icon { get; private set; }
        public static string Description { get; private set; }
        public static string Temp { get; private set; }
        public static string TempUnit { get; private set; }
        public static string Place { get; private set; }
        public static int ExitCode { get; private set; } = -1;

        private static Weather Deserialize(string jsonString)
        {
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true
            };

            return JsonSerializer.Deserialize<Weather>(jsonString);
        }

        public static async void GetWeatherDetails()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    string weatherURL = $"https://dataservice.accuweather.com/currentconditions/v1/{CityID}?apikey={ApiKey}";
                    var response = await httpClient.GetAsync(weatherURL);
                    response.EnsureSuccessStatusCode();
                    string responseString =  await response.Content.ReadAsStringAsync();
                    responseString = responseString.Remove(0, 1);
                    responseString = responseString.Remove(responseString.Length - 1, 1);

                    Weather weather = Deserialize(responseString);

                    Icon = weather.WeatherIcon;
                    Description = weather.WeatherText;
                    Temp = Convert.ToString(weather.Temperature["Metric"]["Value"]);
                    Temp = Convert.ToString(Math.Round(Convert.ToDouble(Temp)));
                    TempUnit = "°C/F";
                    Place = Town + ", " + State;

                    EndProcess();
                }
                catch (System.Exception)
                {
                    if (!IsLocationAvailable)
                        ErrorMessage = "Please turn on your device's location services and refresh the app";
                    else
                        ErrorMessage = "There was a network problem, kindly refresh the app. If the problem persists, then that means the daily limit of API Calls has been exhausted, so then please wait till Subham renews the API";

                    EndProcessWithError();
                }
            }
        }

        public static void ToggleTemperatureUnit()
        {
            if (TempUnit == "°C/F")
            {
                double tempC = Convert.ToDouble(Temp);
                double tempF = Math.Round(((tempC / 5) *  9) + 32);
                Temp = Convert.ToString(tempF);
                TempUnit = "°F/C";
            }
            else
            {
                double tempF = Convert.ToDouble(Temp);
                double tempC = Math.Round(((tempF - 32) / 9) * 5);
                Temp = Convert.ToString(tempC);
                TempUnit = "°C/F";
            }
        }

        public static void EndProcess() => ExitCode = 0;

        public static void EndProcessWithError() => ExitCode = 1;
    }
}