using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CobraEntities
{
    public class ProductMetaData
    {
        public int Id { get; set; }
        public string Namear { get; set; }
        public string Nameen { get; set; }
        public string keyname { get; set; }
        public int MainCategoryID { get; set; }
        public int MainCategoryType { get; set; }
        public int OriginId { get; set; }
        public string mainImageName { get; set; }
        public string MaterialAvilability { get; set; }
        public string SurfaceFinishes { get; set; }

        public string CountryOfOrigin { get; set; }


    }
}
