using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SvarosNamai.Web.Models;
using SvarosNamai.Web.Service.IService;
using static SvarosNamai.Web.Utility.SD;

namespace SvarosNamai.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IInvoiceService _invoice;
        public OrderController(IOrderService orderService, IProductService productService, IInvoiceService invoice)
        {
            _orderService = orderService;
            _productService = productService;
            _invoice = invoice;
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


                foreach (var line in productsWithNoOrderLines)
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
            if (response.IsSuccess)
            {
                var orderLineResponse = await _orderService.GetOrderLines(info.OrderId);
                if (orderLineResponse.IsSuccess)
                {
                    IEnumerable<OrderLineDto> orderLines = JsonConvert.DeserializeObject<IEnumerable<OrderLineDto>>(orderLineResponse.Result.ToString());
                    string text = "Papildomos paslaugos: \n \n";

                    foreach (var line in orderLines)
                    {
                        text += $"Pavadinimas: {line.ProductName}, Kaina: {line.Price}";
                    }

                    OrderStatusChangeDto order = new()
                    {
                        orderId = info.OrderId,
                        status = 5,
                        message = text

                    };
                    var emailSend = await _orderService.ChangeOrderStatus(order);

                    if (emailSend.IsSuccess)
                    {
                        TempData["success"] = "Paslauga pridėta, laiškas išsiųstas";
                        return RedirectToAction("BundleProductsToAdd", new { orderId = info.OrderId });
                    }
                    else
                    {
                        TempData["success"] = "Paslauga pridėta, laiškas neišsiųstas";
                        return RedirectToAction("BundleProductsToAdd", new { orderId = info.OrderId });
                    }

                }
                else
                {
                    TempData["success"] = "Paslauga pridėta, laiškas neišsiųstas";
                    return RedirectToAction("BundleProductsToAdd", new { orderId = info.OrderId });
                }
            }

            else
            {
                TempData["error"] = response.Message;
                return RedirectToAction("BundleProductsToAdd", new { orderId = info.OrderId });
            }
        }


        [Authorize]
        public async Task<IActionResult> OrderIndex()
        {
            List<OrderDto>? list = new();
            ResponseDto response = await _orderService.GetAllOrdersAsync();

            if (response != null && response.IsSuccess)
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


            if (status == -1)
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
            if (!response.IsSuccess)
            {
                TempData["error"] = response?.Message;
                return RedirectToAction("Details", new { orderId = orderId });

            }
            if (status == -1)
            {
                TempData["success"] = "Užsakymas atšauktas";
            }
            else
            {
                TempData["success"] = $"Užsakymo statusas atnaujintas \n {response.Message} ";
            }

            return RedirectToAction("Details", new { orderId = orderId });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<OrderDto> list;


            ResponseDto response = _orderService.GetAllOrdersAsync().GetAwaiter().GetResult();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<OrderDto>>(response.Result.ToString());
            }
            else
            {
                list = new List<OrderDto>();
            }
            return Json(new { data = list });
        }

        [HttpGet]
        public IActionResult GetStatusDescription([FromQuery] int statusCode)
        {
            switch (statusCode)
            {
                case (int)OrderStatus.Pending:
                    return Json("Laukiantis patvirtinimo");
                case (int)OrderStatus.Approved:
                    return Json("Patvirtintas");
                case (int)OrderStatus.Completed:
                    return Json("Užbaigtas");
                case (int)OrderStatus.Cancelled:
                    return Json("Atšauktas");
                default:
                    return Json("Nežinomas");
            }
        }

        [HttpPost("DownloadInvoice")]
        public async Task<IActionResult> DownloadFile(int orderId)
        {

            ResponseDto response = await _invoice.GetInvoice(orderId);

            if (response != null && response.IsSuccess)
            {
                byte[] bytes = JsonConvert.DeserializeObject<byte[]>(response.Result.ToString());
                var stream = new MemoryStream(bytes);

                Response.Headers.Add("Content-Disposition", $"attachment; filename={orderId}.pdf");
                return new FileStreamResult(stream, "application/octet-stream");

            }
            else
            {
                TempData["error"] = "SF neegzistuoja";
                return RedirectToAction("Details", new { orderId = orderId });
            }
        }

        public async Task<IActionResult> OrderPreview(OrderDto order)
        {
            return View();
        }

        public async Task<IActionResult> OrderCreate(bool isCompany)
        {

            ReservationsIntervalDto dates = new()
            {
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today).AddDays(7)

            };

            ResponseDto reservationResponse = await _orderService.GetReservations(dates);
            ResponseDto bundlesResponse = await _productService.GetAllActiveBundles();
            ResponseDto productsResponse = await _productService.GetAllProductsAsync(); 


            if (reservationResponse.IsSuccess && bundlesResponse.IsSuccess && productsResponse.IsSuccess)
            {
                IEnumerable<ReservationsDto> reservations = JsonConvert.DeserializeObject<IEnumerable<ReservationsDto>>(reservationResponse.Result.ToString());

                var availableDates = new List<(DateOnly Date, int Hour)>();

                for(var date = dates.StartDate; date <= dates.EndDate; date = date.AddDays(1))
                {
                    for (int hour = 10; hour < 18; hour++)
                    {
                        if(!reservations.Any(r => r.Date == date && r.Hour == hour))
                        {
                            availableDates.Add((date, hour));
                        }
                    }
                }

                IEnumerable<BundleDto> bundles = JsonConvert.DeserializeObject<IEnumerable<BundleDto>>(bundlesResponse.Result.ToString());

                IEnumerable<ProductDto> products = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(productsResponse.Result.ToString());

                ViewBag.Products = products;

                ViewBag.Bundles = bundles;

                ViewBag.AvailableDates = availableDates
                .Select(dh => $"{dh.Date.ToString("yyyy-MM-dd")}, {dh.Hour}")
                .ToList();

            }
            else
            {
                return NotFound();
            }

            OrderDto order = new OrderDto();
            order.IsCompany = isCompany;

            return View(order);
        }


    }
}
