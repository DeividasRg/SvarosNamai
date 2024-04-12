namespace SvarosNamai.Web.Models
{
    public class ProductPricesViewDto
    {
        public int OrderId { get; set; }
        public IEnumerable<OrderLineDto> Lines { get; set; }
    }
}
