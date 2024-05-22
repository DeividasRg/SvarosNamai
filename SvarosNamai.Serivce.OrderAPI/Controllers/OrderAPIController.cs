using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SvarosNamai.Serivce.OrderAPI.Models;
using SvarosNamai.Serivce.OrderAPI.Models.Dtos;
using SvarosNamai.Serivce.OrderAPI.Service.IService;
using SvarosNamai.Serivce.OrderAPI.Utility;
using SvarosNamai.Service.OrderAPI.Data;
using SvarosNamai.Service.OrderAPI.Models.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SvarosNamai.Serivce.OrderAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IEmailService _email;
        private readonly IErrorLogger _error;
        private readonly IInvoiceService _invoice;

        public OrderAPIController(AppDbContext db, IMapper mapper, IProductService productService, IEmailService email, IErrorLogger error, IInvoiceService invoice)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
            _productService = productService;
            _email = email;
            _error = error;
            _invoice = invoice;
        }


        [HttpPost("CreateOrder")]
        public async Task<ResponseDto> CreateOrder(OrderDto order, int bundleId)
        {
            try
            {
                BundleDto bundleFromDb = await _productService.GetBundle(bundleId);
                if (bundleFromDb != null)
                {
                    Order orderToDb = _mapper.Map<Order>(order);
                    orderToDb.Price = ((order.SquareFoot * 2.4) / 60) * bundleFromDb.HourPrice;
                    await _db.Orders.AddAsync(orderToDb);
                    await _db.SaveChangesAsync();

                    //Send An Email
                    var emailSend = await _email.SendConfirmationEmail(_mapper.Map<ConfirmationEmailDto>(orderToDb));


                    if (emailSend.IsSuccess)
                    {
                        _response.Message = "Order Created";
                    }
                    else
                    {
                        _response.Message = $"Order Created, Email not sent. Reason: {emailSend.Message}";
                        _error.LogError(emailSend.Message);
                        return _response;
                    }
                }
                else
                {
                    throw new Exception("Bundle doesn't exist or doesn't have products");
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _error.LogError(ex.Message);
            }
            return _response;
        }

        [Authorize]
        [HttpPut("OrderStatusChange")]
        public async Task<ResponseDto> OrderStatusChange(OrderStatusChangeDto orderInfo)
        {
            try
            {
                Order orderCheck = _db.Orders.Find(orderInfo.orderId);
                if (orderCheck != null)
                {
                    int statusCheck = OrderStatusses.GetStatusConstant(orderInfo.status);
                    if (statusCheck == 99)
                    {
                        _response.Message = "Not a valid status";
                        _response.IsSuccess = false;
                        _error.LogError(_response.Message);
                        return _response;
                    }
                    else if (statusCheck == orderCheck.Status)
                    {
                        _response.Message = "Order status is as submitted";
                        _response.IsSuccess = false;
                        _error.LogError(_response.Message);
                        return _response;
                    }
                    else
                    {
                        ConfirmationEmailDto info = _mapper.Map<ConfirmationEmailDto>(orderCheck);

                        switch (orderInfo.status)
                        {
                            case OrderStatusses.Status_Approved:

                                if (orderCheck.Status == OrderStatusses.Status_Completed)
                                {
                                    throw new Exception("Order already completed");
                                }

                                orderCheck.Status = OrderStatusses.Status_Approved;
                                info.OrderStatus = OrderStatusses.Status_Approved;
                                var emailSendForApproved = await _email.SendConfirmationEmail(info);
                                if (!emailSendForApproved.IsSuccess)
                                {
                                    _response.Message = "Neišsiųstas laiškas";
                                }
                                await _db.SaveChangesAsync();

                                OrderLog orderLogApproved = new()
                                {
                                    OrderId = orderCheck.OrderId,
                                    NewOrderStatus = OrderStatusses.Status_Approved,
                                    Email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                                    Time = DateTime.Now
                                };
                                await _db.OrderLogs.AddAsync(orderLogApproved);
                                await _db.SaveChangesAsync();

                                break;
                            case OrderStatusses.Status_Cancelled:

                                if (orderCheck.Status == OrderStatusses.Status_Completed)
                                {
                                    throw new Exception("Order already completed");
                                }
                                info.message = orderInfo.message;

                                orderCheck.Status = OrderStatusses.Status_Cancelled;
                                orderCheck.Reservation.IsActive = false;
                                info.OrderStatus = OrderStatusses.Status_Cancelled;
                                var emailSendForCancelled = await _email.SendConfirmationEmail(info);
                                await _db.SaveChangesAsync();

                                OrderLog orderLogCancelled = new()
                                {
                                    OrderId = orderCheck.OrderId,
                                    NewOrderStatus = OrderStatusses.Status_Cancelled,
                                    Email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                                    Time = DateTime.Now
                                };
                                _db.OrderLogs.AddAsync(orderLogCancelled);
                                await _db.SaveChangesAsync();

                                break;
                            case OrderStatusses.Status_Completed:
                                OrderForInvoiceDto order = _mapper.Map<OrderForInvoiceDto>(orderCheck);
                                order.Lines = _mapper.Map<IEnumerable<OrderLinesForInvoiceDto>>(_db.OrderLines.Where(u => u.Order.OrderId == orderCheck.OrderId));

                                foreach (var line in order.Lines)
                                {
                                    if (line.Price == 0 || line.Price == null)
                                    {
                                        throw new Exception("Not all product lines have a price");
                                    }
                                }

                                order.Status = OrderStatusses.Status_Completed;
                                ResponseDto generateInvoice = await _invoice.GenerateInvoice(order);
                                if (generateInvoice.IsSuccess)
                                {
                                    info.pdfFile = JsonConvert.DeserializeObject<byte[]>(generateInvoice.Result.ToString());
                                    var emailConfirmationSend = await _email.SendCompleteEmail(info);
                                    if (!emailConfirmationSend.IsSuccess)
                                    {
                                        _response.Message = "Neišsiųstas laiškas";
                                        orderCheck.Status = OrderStatusses.Status_Completed;
                                        await _db.SaveChangesAsync();
                                    }
                                    else
                                    {
                                        orderCheck.Status = OrderStatusses.Status_Completed;
                                        await _db.SaveChangesAsync();
                                    }

                                    OrderLog orderLogCompleted = new()
                                    {
                                        OrderId = orderCheck.OrderId,
                                        NewOrderStatus = OrderStatusses.Status_Completed,
                                        Email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                                        Time = DateTime.Now
                                    };
                                    _db.OrderLogs.AddAsync(orderLogCompleted);
                                    await _db.SaveChangesAsync();

                                }
                                else
                                {
                                    throw new Exception($"{generateInvoice.Message}");
                                }
                                break;
                            case OrderStatusses.Status_Addition:
                                info.OrderStatus = OrderStatusses.Status_Addition;
                                info.message = orderInfo.message;
                                ResponseDto response = await _email.SendConfirmationEmail(info);
                                if (!response.IsSuccess)
                                {
                                    throw new Exception("Laiškas neišsiųstas");
                                }

                                OrderLog orderLogAddition = new()
                                {
                                    OrderId = orderCheck.OrderId,
                                    NewOrderStatus = OrderStatusses.Status_Addition,
                                    Email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                                    Time = DateTime.Now
                                };
                                _db.OrderLogs.AddAsync(orderLogAddition);
                                await _db.SaveChangesAsync();

                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _error.LogError(ex.Message);

            }
            return _response;
        }

        [Authorize]
        [HttpDelete("RemoveProductFromOrder")]
        public async Task<ResponseDto> RemoveProductFromOrder(ProductOrderDto info)
        {
            try
            {
                var orderCheck = await _db.Orders.FindAsync(info.orderId);
                var productCheck = await _productService.GetProductByName(info.productName);
                if (orderCheck != null && productCheck.IsSuccess)
                {
                    ProductDto product = JsonConvert.DeserializeObject<ProductDto>(productCheck.Result.ToString());

                    OrderLine orderLineCheck = _db.OrderLines.FirstOrDefault(u => u.Order.OrderId == info.orderId && u.ProductName == product.Name);
                    if (orderLineCheck != null)
                    {
                        _db.OrderLines.Remove(orderLineCheck);
                        await _db.SaveChangesAsync();
                        _response.Message = "Removed";
                        return _response;
                    }
                    else
                    {
                        throw new Exception("No orderline to delete");
                    }
                }
                else
                {
                    throw new Exception("Order or Product doesn't exist");
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _error.LogError(ex.Message);
                return _response;
            }
        }

        [Authorize]
        [HttpPost("AddProductToOrder")]
        public async Task<ResponseDto> AddProductToOrder(ProductOrderDto info)
        {
            try
            {
                var orderCheck = await _db.Orders.FindAsync(info.orderId);
                var productCheck = await _productService.GetProduct(info.productId);



                if (orderCheck != null && productCheck.IsSuccess)
                {
                    ProductDto product = JsonConvert.DeserializeObject<ProductDto>(productCheck.Result.ToString());

                    bool orderLineCheck = _db.OrderLines.Any(u => u.Order.OrderId == info.orderId && u.ProductName == product.Name);
                    if (orderLineCheck)
                    {
                        throw new Exception("OrderLine already exists");
                    }

                    OrderLine line = new OrderLine()
                    {
                        Order = orderCheck,
                        ProductName = product.Name,
                        Price = 0
                    };

                    await _db.OrderLines.AddAsync(line);
                    await _db.SaveChangesAsync();

                    _response.Message = "Successfully added";
                    return _response;
                }
                else
                {
                    throw new Exception("Order or Product doesn't exist");
                }
            }
            catch (Exception ex)
            {
                _error.LogError(ex.Message);
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return _response;
            }
        }



        [Authorize]
        [HttpGet("GetOrders")]
        public async Task<ResponseDto> GetOrders()
        {
            IEnumerable<Order> orders = _db.Orders.ToList();
            List<OrderDto> orderDtos = new List<OrderDto>();

            foreach (var order in orders)
            {
                var orderDto = _mapper.Map<OrderDto>(order);
                orderDto.Hour = order.Reservation.Hour;
                orderDto.Date = order.Reservation.Date;
                orderDtos.Add(orderDto);
            }

            _response.Result = orderDtos;
            return _response;
        }

        [Authorize]
        [HttpGet("GetOrder/{orderId}")]
        public async Task<ResponseDto> GetOrder(int orderId)
        {
            Order order = _db.Orders.Find(orderId);
            OrderDto orderDto = _mapper.Map<OrderDto>(order);
            _response.Result = orderDto;
            return _response;
        }

        [HttpGet("GetOrderlines/{orderId}")]
        public async Task<ResponseDto> GetOrderLines(int orderId)
        {
            IEnumerable<OrderLine> orderLines = _db.OrderLines.Where(u => u.Order.OrderId == orderId).ToList();
            IEnumerable<OrderLineDto> orderForSend = _mapper.Map<IEnumerable<OrderLineDto>>(orderLines);
            _response.Result = orderForSend;
            return _response;
        }

        [HttpGet("GetReservations")]
        public async Task<ResponseDto> GetReservations(ReservationsIntervalDto dates)
        {
            try
            {
                IEnumerable<Reservations> reservations = _db.Reservations
                             .Where(r => r.Date >= dates.StartDate && r.Date < dates.EndDate && r.IsActive && r.Hour >= 10 && r.Hour <= 17)
                             .OrderBy(r => r.Date)
                             .ThenBy(r => r.Hour)
                             .ToList();

                _response.Result = _mapper.Map<IEnumerable<ReservationsDto>>(reservations);


            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _error.LogError(ex.Message);
            }
            return _response;
        }


    }
}
