using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SvarosNamai.Serivce.OrderAPI.Models;
using SvarosNamai.Serivce.OrderAPI.Service.IService;
using SvarosNamai.Service.OrderAPI.Data;
using SvarosNamai.Service.OrderAPI.Models.Dtos;
using System;
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

        public OrderAPIController(AppDbContext db, IMapper mapper, IProductService productService)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
            _productService = productService;
        }


        [HttpPost("CreateOrder")]
        public async Task<ResponseDto> CreateOrder(OrderDto order, int bundleId)
        {

        }
    }
}
