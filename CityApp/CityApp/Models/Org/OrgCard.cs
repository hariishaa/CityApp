using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.Models.Org
{
    public class OrgCard
    {
        public int city_id { get; set; }
        public string city { get; set; }
        public int street_id { get; set; }
        public string street { get; set; }
        public string building_num { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cat_id { get; set; }
        public int subcat_id { get; set; }
        public string lon { get; set; }
        public string lat { get; set; }
        public string description { get; set; }
    }
}
