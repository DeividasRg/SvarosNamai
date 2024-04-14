namespace SvarosNamai.Web.Models
{
    public class ProductPricesViewDto
    {
        public int OrderId { get; set; }
        public List<OrderLineDto>? Lines { get; set; }
        public int productId { get; set; }

        public ProductPricesViewDto()
        {
            Lines = new List<OrderLineDto>();
        }
    }
}
