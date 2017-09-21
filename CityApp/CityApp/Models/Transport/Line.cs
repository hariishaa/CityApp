using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.Models.Transport
{
    public class Line : Route
    {
        public int line_id { get; set; }
        public string remark { get; set; }
    }
}
