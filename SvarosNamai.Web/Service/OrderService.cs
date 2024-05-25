using SvarosNamai.Web.Models;
using SvarosNamai.Web.Service.IService;
using SvarosNamai.Web.Utility;
using System;

namespace SvarosNamai.Web.Service
{
    public class OrderService : IOrderService
    {

        private readonly IBaseService _baseService;

        public OrderService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> AddProductToOrder(ProductOrderDto info)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = info,
                Url = SD.OrderAPIBase + "/api/order/AddProductToOrder"
            });
        }

        public async Task<ResponseDto> ChangeOrderStatus(OrderStatusChangeDto orderInfo)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = orderInfo,
                Url = SD.OrderAPIBase + "/api/order/OrderStatusChange"
            });

        }

        public async Task<ResponseDto> CreateOrder(CreateOrderDto order)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = order,
                Url = SD.OrderAPIBase + "/api/order/CreateOrder"
            });
        }

        public async Task<ResponseDto?> GetAllOrdersAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.OrderAPIBase + "/api/order/GetOrders"
            });
        }

		public async Task<ResponseDto?> GetOrderAsync(int orderId)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.OrderAPIBase + "/api/order/GetOrder/" + orderId
			});
		}

        public async Task<ResponseDto?> GetOrderLines(int orderId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.OrderAPIBase + "/api/order/GetOrderlines/" + orderId
            });
        }

        public async Task<ResponseDto> GetReservations(ReservationsIntervalDto dates)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Data = dates,
                Url = SD.OrderAPIBase + "/api/order/GetReservations"
            });
        }

        public async Task<ResponseDto> RemoveProductFromOrder(ProductOrderDto info)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Data = info,
                Url = SD.OrderAPIBase + "/api/order/RemoveProductFromOrder"
            });
        }

    }
}
