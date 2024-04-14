namespace SvarosNamai.Serivce.OrderAPI.Models.Dtos
{
    public class ProductOrderDto
    {
        public int orderId {get; set;}
        public int productId { get; set; } = 0;
        public string? productName { get; set; }
    }
}
