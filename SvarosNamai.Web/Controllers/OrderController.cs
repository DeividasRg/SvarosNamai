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
        [HttpGet]
        public async Task<IActionResult> ProductPrices(int orderId)
        {
            List<OrderLineDto> lines = new List<OrderLineDto>();
            ResponseDto linesResponse = await _orderService.GetOrderLines(orderId);

            if(linesResponse != null && linesResponse.IsSuccess)
            {
                lines = JsonConvert.DeserializeObject<List<OrderLineDto>>(linesResponse.Result.ToString());
                ProductPricesViewDto products = new()
                {
                    Lines = lines,
                    OrderId = orderId
                };
                return View(products);
            }
            return RedirectToAction("Details", new { orderId = orderId });

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductPrices([FromForm]ProductPricesViewDto products)
        {

            int orderId = products.OrderId;
            ResponseDto response = await _orderService.ChangeProductPrices(products.Lines);

            if(response != null && response.IsSuccess)
            {
                return RedirectToAction("Details", new { orderId = orderId });
            }

            return RedirectToAction("Details", new {orderId = orderId});
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
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(int orderId)
        {
            OrderDto order = new();
            IEnumerable<OrderLineDto> lines = new List<OrderLineDto>();
            ResponseDto orderResponse = await _orderService.GetOrderAsync(orderId);
            ResponseDto linesResponse = await _orderService.GetOrderLines(orderId);
            

            if (orderResponse != null && orderResponse.IsSuccess && linesResponse != null && linesResponse.IsSuccess)
            {
                order = JsonConvert.DeserializeObject<OrderDto>(orderResponse.Result.ToString());
                lines = JsonConvert.DeserializeObject<IEnumerable<OrderLineDto>>(linesResponse.Result.ToString());
            }

            OrderDetailDto orderDetailDto = new OrderDetailDto
            {
                Order = order,
                Lines = lines
            };
                return View(orderDetailDto);
        }

        [Authorize]
        public async Task<IActionResult> ChangeOrderStatus(int orderId, int status)
        {
            OrderStatusChangeDto order = new OrderStatusChangeDto();

            if (status == 1)
            {
                var responseDto = await _orderService.GetOrderLines(orderId);
                IEnumerable<OrderLineDto> orderLines = JsonConvert.DeserializeObject<IEnumerable<OrderLineDto>>(responseDto.Result.ToString());
                string messageForSend = "Pasiūlymas: \n";

                foreach (var line in orderLines)
                {
                    messageForSend += $"{line.ProductName} : {line.Price} € \n";
                }
                
                order.orderId = orderId;
                order.status = status;
                order.message = messageForSend;
                

            }
            else
            {
                order.orderId = orderId;
                order.status = status;
            }

            ResponseDto response = await _orderService.ChangeOrderStatus(order);

            return RedirectToAction("Details", new { orderId = orderId });
        }
    }
}
