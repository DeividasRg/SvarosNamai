namespace SvarosNamai.Web.Models
{
    public class OrderPreviewDto
    {
        public OrderDto Order {  get; set; }
        public BundleDto Bundle { get; set; }
        public ProductDto? Product { get; set; }

    }
}
