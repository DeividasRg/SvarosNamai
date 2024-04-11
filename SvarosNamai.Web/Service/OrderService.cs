using SvarosNamai.Web.Models;
using SvarosNamai.Web.Service.IService;
using SvarosNamai.Web.Utility;

namespace SvarosNamai.Web.Service
{
    public class OrderService : IOrderService
    {

        private readonly IBaseService _baseService;

        public OrderService(IBaseService baseService)
        {
            _baseService = baseService;
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
