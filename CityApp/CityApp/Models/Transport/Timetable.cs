using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.Models.Transport
{
    public class Timetable : Line
    {
        public int day_id { get; set; }
        public string day_short_name { get; set; }
        public int timetable_id { get; set; }
        public string hour { get; set; }
        public string minutes { get; set; }
    }
}