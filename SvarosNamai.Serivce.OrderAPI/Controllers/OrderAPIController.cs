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
        private IEmailService _emaill;

        public OrderAPIController(AppDbContext db, IMapper mapper, IProductService productService, IInvoiceGenerator invoice, IEmailService emaill)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
            _productService = productService;
            _invoice = invoice;
            _emaill = emaill;
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

                    var emailSend = await _emaill.SendConfirmationEmail(new ConfirmationEmailDto()
                    {
                        Email = orderToDb.Email,
                        Name = orderToDb.Name,
                        LastName = orderToDb.LastName,
                        OrderId = orderToDb.OrderId
                    });

                    if(emailSend.IsSuccess)
                    {
                        _response.Message = "Order Created";
                    }
                    else
                    {
                        _response.Message = $"Order Created, Email not sent. Reason: {emailSend.Message}";
                    }


                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
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
                    }
                    else if (statusCheck == orderCheck.Status)
                    {
                        _response.Message = "Order status is as submitted";
                        _response.IsSuccess = false;
                    }
                    else
                    {
                        switch (status)
                        {
                            case OrderStatusses.Status_Approved:
                                orderCheck.Status = OrderStatusses.Status_Approved;
                                await _db.SaveChangesAsync();
                                break;
                            case OrderStatusses.Status_Cancelled:
                                orderCheck.Status = OrderStatusses.Status_Cancelled;
                                orderCheck.Reservation.IsActive = false;
                                await _db.SaveChangesAsync();
                                //Send Email explaining why
                                break;
                            case OrderStatusses.Status_Completed:
                                var generateInvoice = await _invoice.GenerateInvoice(orderId);
                                if (generateInvoice.IsSuccess)
                                {
                                    orderCheck.Status = OrderStatusses.Status_Completed;
                                    await _db.SaveChangesAsync();
                                }
                                else
                                {
                                    break;
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
            }
            return _response;
        }
    }
}
