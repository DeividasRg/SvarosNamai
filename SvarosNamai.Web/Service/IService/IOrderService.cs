using SvarosNamai.Web.Models;

namespace SvarosNamai.Web.Service.IService
{
    public interface IOrderService
    {
        Task<ResponseDto?> GetAllOrdersAsync();
        Task<ResponseDto?> GetOrderAsync(int orderId);
        Task<ResponseDto?> GetOrderLines(int orderId);
        Task<ResponseDto> ChangeOrderStatus(OrderStatusChangeDto orderInfo);
        Task<ResponseDto> ChangeProductPrices(IEnumerable<OrderLineDto> lines);
        Task<ResponseDto> AddProductToOrder(ProductToAddToOrderDto info);
    }
}
