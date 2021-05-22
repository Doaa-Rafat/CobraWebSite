using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CobraWebSite.Models
{
    public class ProductDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MainCategoryID { get; set; }
        public int MainCategoryType { get; set; }
        public int OriginId { get; set; }
        public string CountryOfOrigin { get; set; }
        public string mainImageName { get; set; }
        public string Color { get; set; }
        public string MaterialAvilability { get; set; }
        public string SurfaceFinishes { get; set; }

        public string CompressiveStrength { get; set; }
        public string FlexuralStrength { get; set; }
        public string AbrasionResistanceHardness { get; set; }
        public string Density { get; set; }
        public string WaterAbsorption { get; set; }
        public string ModulusofRupture { get; set; }
        
        public List<ProductPrice> PriceDetails { get; set; }
        public List<ProductImage> Images { get; set; }
        public List<ProductVideo> Videos { get; set; }

        public List<RelatedProduct> RelatedProducts { get; set; }
    }
}
