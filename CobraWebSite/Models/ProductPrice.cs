using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CobraWebSite.Models
{
    public class ProductPrice
    {
        public int FkproductId { get; set; }

        public int Price { get; set; }
        public string PriceUnit { get; set; }
        public int Thickness { get; set; }
        public int FKMaterialDescriptionId { get; set; }
        public string MaterialDescription { get; set; }
        public string notes { get; set; }
    }
}
