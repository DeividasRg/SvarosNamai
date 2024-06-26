﻿using SvarosNamai.Web.Models;

namespace SvarosNamai.Web.Service.IService
{
    public interface IOrderService
    {
        Task<ResponseDto?> GetAllOrdersAsync();
        Task<ResponseDto?> GetOrderAsync(int orderId);
        Task<ResponseDto?> GetOrderLines(int orderId);
        Task<ResponseDto> ChangeOrderStatus(OrderStatusChangeDto orderInfo);
        Task<ResponseDto> AddProductToOrder(ProductOrderDto info);
        Task<ResponseDto> RemoveProductFromOrder(ProductOrderDto info);
        Task<ResponseDto> CreateOrder(IEnumerable<CreateOrderDto> order);
        Task<ResponseDto> GetTimeslots();
    }
}
