namespace SvarosNamai.Web.Models
{
    public class ProductOrderDto
    {
        public int orderId {get; set;}
        public int productId { get; set; } = 0;
        public string? productName { get; set;}
        public double? Price { get; set; }
    }
}
