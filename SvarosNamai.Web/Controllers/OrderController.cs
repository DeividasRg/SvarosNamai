using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SvarosNamai.Web.Models;
using SvarosNamai.Web.Service.IService;

namespace SvarosNamai.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        public OrderController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        [Authorize]
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
            else
            {
                TempData["error"] = linesResponse.Message;
            }
            return RedirectToAction("Details", new { orderId = orderId });

        }



        [Authorize]
        public async Task<IActionResult> BundleProductsToAdd(int orderId)
        {
            List<OrderLineDto> lines = new List<OrderLineDto>();
            ResponseDto linesResponse = await _orderService.GetOrderLines(orderId);

            if (linesResponse != null && linesResponse.IsSuccess)
            {
                lines = JsonConvert.DeserializeObject<List<OrderLineDto>>(linesResponse.Result.ToString());
                ProductPricesViewDto products = new()
                {
                    Lines = lines,
                    OrderId = orderId
                };

                var productResponse = await _productService.GetAllProductsAsync();
                IEnumerable<ProductDto> allProductList = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(productResponse.Result.ToString());
                IEnumerable<ProductDto> productsWithNoOrderLines = allProductList
                .Where(product => !products.Lines.Any(line => line.ProductName == product.Name));

                var productListToAdd = new List<SelectListItem>();


                foreach(var line in productsWithNoOrderLines)
                {
                    productListToAdd.Add(new SelectListItem { Text = line.Name, Value = line.ProductId.ToString() });
                };

                ViewBag.Products = productListToAdd;


                return View(products);
            }
            else
            {
				TempData["error"] = linesResponse.Message;
			}
            
            

            return RedirectToAction("Details", new { orderId = orderId });

        }

        [Authorize]
        public async Task<IActionResult> RemoveProductFromOrder(int orderId, string productName)
        {
            ProductOrderDto product = new()
            {
                productName = productName,
                orderId = orderId
            };

            var response = await _orderService.RemoveProductFromOrder(product);
            if (response.IsSuccess)
            {
                TempData["success"] = "Paslauga išimta";
                return RedirectToAction("BundleProductsToAdd", new { orderId = orderId });
            }
            else
            {
				TempData["error"] = response.Message;
				throw new Exception(response.Message);
            }

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> BundleProductsToAdd(ProductPricesViewDto info)
        {
            ProductOrderDto product = new()
            {
                productId = info.productId,
                orderId = info.OrderId
            };

            var response = await _orderService.AddProductToOrder(product);
            if(response.IsSuccess)
            {
                TempData["success"] = "Paslauga pridėta";
                return RedirectToAction("BundleProductsToAdd", new {orderId = info.OrderId});
            }
            else
            {
				TempData["error"] = response.Message;
				throw new Exception(response.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductPrices([FromForm]ProductPricesViewDto products)
        {

            int orderId = products.OrderId;
            ResponseDto response = await _orderService.ChangeProductPrices(products.Lines);

            if(response != null && response.IsSuccess)
            {
                TempData["success"] = "Kainos atnaujintos";
                return RedirectToAction("Details", new { orderId = orderId });
            }
			TempData["error"] = response.Message;
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
            else
            {
			    TempData["error"] = response.Message;
            }
			return View(list);
        }
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
            else
            {
				TempData["error"] = orderResponse.Message + linesResponse.Message;
			}

            OrderDetailDto orderDetailDto = new OrderDetailDto
            {
                Order = order,
                Lines = lines
            };
                return View(orderDetailDto);
        }

        [Authorize]
        public async Task<IActionResult> DetailsToCancel(int orderId)
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
            else
            {
                TempData["error"] = orderResponse.Message + linesResponse.Message;
            }

            OrderDetailDto orderDetailDto = new OrderDetailDto
            {
                Order = order,
                Lines = lines
            };
            return View(orderDetailDto);
        }

        [Authorize]
        public async Task<IActionResult> ChangeOrderStatus(int orderId, int status, string? message)
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
            else if(status == -1)
            {
                order.orderId = orderId;
                order.status = status;
                order.message = message;
            }
            else
            {
                order.orderId = orderId;
                order.status = status;
            }

            ResponseDto response = await _orderService.ChangeOrderStatus(order);
            if(!response.IsSuccess)
            {
                TempData["error"] = response?.Message;

			}
            if(status == -1)
            {
                TempData["success"] = "Užsakymas atšauktas";
            }
            else
            {
                TempData["success"] = "Užsakymo statusas atnaujintas";
            }

            return RedirectToAction("Details", new { orderId = orderId });
        }


    }
}
