namespace SvarosNamai.Web.Models
{
    public class CreateOrderDto
    {
        public OrderDto Order { get; set; }
        public ProductDto Product { get; set; }
        public BundleDto Bundle { get; set; }
    }
}
