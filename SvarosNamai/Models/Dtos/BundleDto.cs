using System.ComponentModel.DataAnnotations;

namespace SvarosNamai.Service.ProductAPI.Models.Dtos
{
    public class BundleDto
    {
        public string BundleName { get; set; }
        public double Price { get; set; }
        public double? Discount { get; set; }
    }
}
