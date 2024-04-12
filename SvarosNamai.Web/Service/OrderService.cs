﻿using SvarosNamai.Web.Models;
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

        public async Task<ResponseDto> ChangeOrderStatus(OrderStatusChangeDto orderInfo)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = orderInfo,
                Url = SD.OrderAPIBase + "/api/order/OrderStatusChange"
            });

        }

        public async Task<ResponseDto> ChangeProductPrices(IEnumerable<OrderLineDto> lines)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = lines,
                Url = SD.OrderAPIBase + "/api/order/AddPricesToProducts"
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
    }
}
