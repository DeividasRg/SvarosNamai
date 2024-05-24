using SvarosNamai.Service.OrderAPI.Models.Dtos;

namespace SvarosNamai.Serivce.OrderAPI.Models.Dtos
{
    public class CreateOrderDto
    {
        public OrderDto Order { get; set; }
        public ProductDto Product { get; set; }
        public BundleDto Bundle { get; set; }
    }
}
