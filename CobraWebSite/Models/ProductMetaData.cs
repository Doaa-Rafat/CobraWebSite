﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CobraWebSite.Models
{
    public class ProductMetaData
    {
        public int Id { get; set; }
        public string Namear { get; set; }
        public int Nameen { get; set; }
        public int MainCategoryID { get; set; }
        public int MainCategoryType { get; set; }
        public int OriginId { get; set; }
        public string mainImageName { get; set; }

    }
}