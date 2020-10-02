using System.Collections.Generic;

namespace Components
{
    class Weather
    {
        public int WeatherIcon { get; set; }
        public string WeatherText { get; set; }
        public Dictionary<string, Dictionary<string, object>> Temperature { get; set; }
    }
}