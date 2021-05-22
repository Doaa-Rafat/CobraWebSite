using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CobraWebSite.Models
{
    public class ProductVideo
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public int ProductId { get; set; }
    }
}
