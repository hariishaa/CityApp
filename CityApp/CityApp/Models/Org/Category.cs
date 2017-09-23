﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.Models.Org
{
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Subcategory> subcategories { get; set; }
    }
}