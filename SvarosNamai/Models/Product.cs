using System.ComponentModel.DataAnnotations;

namespace SvarosNamai.Service.ProductAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public Bundle BundleId { get; set; }
    }
}
