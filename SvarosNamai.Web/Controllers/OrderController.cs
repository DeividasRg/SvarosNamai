using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SvarosNamai.Web.Models;
using SvarosNamai.Web.Service.IService;

namespace SvarosNamai.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        public async Task<IActionResult> OrderIndex()
        {
            List<OrderDto>? list = new();
            ResponseDto response = await _orderService.GetAllOrdersAsync();

            if(response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<OrderDto>>(response.Result.ToString());
            }

            return View(list);
        }
    }
}
