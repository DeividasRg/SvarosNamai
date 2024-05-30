namespace SvarosNamai.Web.Models
{
    public class OrderPreviewDto
    {
        public List<OrderDto> Orders {  get; set; }
        public BundleDto Bundle { get; set; }
        public ProductDto? Product { get; set; }
        public double FullPrice {  get; set; }
        public bool isCompany { get; set; }

    }
}
