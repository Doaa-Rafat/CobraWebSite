using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CobraWebSite.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int productId { get; set; }
        public int DisplayOrder { get; set; }

    }
}
