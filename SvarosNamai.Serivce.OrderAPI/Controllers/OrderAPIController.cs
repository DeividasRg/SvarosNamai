using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SvarosNamai.Serivce.OrderAPI.Models;
using SvarosNamai.Serivce.OrderAPI.Service.IService;
using SvarosNamai.Serivce.OrderAPI.Utility;
using SvarosNamai.Service.OrderAPI.Data;
using SvarosNamai.Service.OrderAPI.Models.Dtos;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace SvarosNamai.Serivce.OrderAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        protected ResponseDto _response;
        private IMapper _mapper;
        private IProductService _productService;
        private IInvoiceGenerator _invoice;
        private IEmailService _email;
        private IErrorLogger _error;

        public OrderAPIController(AppDbContext db, IMapper mapper, IProductService productService, IInvoiceGenerator invoice, IEmailService email, IErrorLogger error)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
            _productService = productService;
            _invoice = invoice;
            _email = email;
            _error = error;
        }


        [HttpPost("CreateOrder")]
        public async Task<ResponseDto> CreateOrder(OrderDto order, int bundleId)
        {
            try
            {
                BundleDto bundleFromDb = await _productService.GetBundle(bundleId);
                if (bundleFromDb != null && bundleFromDb.Products != null)
                {
                    Order orderToDb = _mapper.Map<Order>(order);
                    orderToDb.CreationDate = DateTime.Now;
                    orderToDb.Price = bundleFromDb.Price;
                    await _db.Orders.AddAsync(orderToDb);



                    foreach (var product in bundleFromDb.Products)
                    {
                        OrderLine orderline = new OrderLine();
                        orderline.Order = orderToDb;
                        orderline.ProductName = product.Name;
                        await _db.OrderLines.AddAsync(orderline);
                        await _db.SaveChangesAsync();
                    }
                    //Send An Email


                    var emailSend = await _email.SendConfirmationEmail(_mapper.Map<ConfirmationEmailDto>(orderToDb));
                    

                    if(emailSend.IsSuccess)
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

        [HttpPost]
        public async Task<ResponseDto> PdfFile(int id)
        {
            var resp = _invoice.GenerateInvoice(id);
            return _response;
        }


        [HttpPut("OrderStatusChange")]
        public async Task<ResponseDto> OrderStatusChange(int status, int orderId)
        {
            try
            {
                Order orderCheck = _db.Orders.Find(orderId);
                if (orderCheck != null)
                {
                    int statusCheck = OrderStatusses.GetStatusConstant(status);
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

                        switch (status)
                        {
                            case OrderStatusses.Status_Approved:
                                orderCheck.Status = OrderStatusses.Status_Approved;
                                info.OrderStatus = OrderStatusses.Status_Approved;
                                var emailSendForApproved = await _email.SendConfirmationEmail(info);
                                await _db.SaveChangesAsync();
                                break;
                            case OrderStatusses.Status_Cancelled:
                                orderCheck.Status = OrderStatusses.Status_Cancelled;
                                orderCheck.Reservation.IsActive = false;
                                info.OrderStatus = OrderStatusses.Status_Cancelled;
                                var emailSendForCancelled = await _email.SendConfirmationEmail(info);
                                await _db.SaveChangesAsync();
                                break;
                            case OrderStatusses.Status_Completed:
                                var generateInvoice = await _invoice.GenerateInvoice(orderId);
                                if (generateInvoice.IsSuccess)
                                {
                                    orderCheck.Status = OrderStatusses.Status_Completed;
                                    var emailConfirmationSend = await _email.SendCompleteEmail(info, generateInvoice.Result.ToString());
                                    if (!emailConfirmationSend.IsSuccess)
                                    {
                                        throw new Exception($"{emailConfirmationSend.Message}")
                                    }
                                    else
                                    {
                                        await _db.SaveChangesAsync();
                                    }
                                }
                                else
                                {
                                    throw new Exception($"{generateInvoice.Message}")
                                }
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
    }
}
