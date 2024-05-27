namespace SvarosNamai.Web.Models
{
    public class OrderPreviewDto
    {
        public IEnumerable<OrderDto> Orders {  get; set; }
        public BundleDto Bundle { get; set; }
        public ProductDto? Product { get; set; }
        public double FullPrice {  get; set; }

    }
}
