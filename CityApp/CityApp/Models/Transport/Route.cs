using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.Models.Transport
{
    public class Route
    {
        public string route_num { get; set; }
        public int route_id { get; set; }
        public int city_from_id { get; set; }
        public string city_from_name { get; set; }
        public int station_from_id { get; set; }
        public string station_from_name { get; set; }
        public int city_to_id { get; set; }
        public string city_to_name { get; set; }
        public int station_to_id { get; set; }
        public string station_to_name { get; set; }
    }
}
