using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidStockChecker.Models
{
    public class ProductsInStock
    {
        public string SKU { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public string Retailer { get; set; }
        public RSC.Models.Type type { get; set; }
        public RSC.Models.Discord discord { get; set; }
    }
}
