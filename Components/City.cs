using System.Collections.Generic;

namespace Components
{
    class City
    {
        public string Key { get; set; }
        public string EnglishName { get; set; }
        public Dictionary<string, object> AdministrativeArea { get; set; }
    }
}