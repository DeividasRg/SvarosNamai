using System.Collections;
using System.Collections.Generic;

namespace SvarosNamai.Service.ProductAPI.Models
{
    public class ProductBundle
    {
        public Bundle BundleId { get; set; }
        public IEnumerable<Product> Products { get; set;}
    }
}
