using SvarosNamai.Web.Models;
using SvarosNamai.Web.Service.IService;
using SvarosNamai.Web.Utility;
using System;
using static SvarosNamai.Web.Utility.SD;

namespace SvarosNamai.Web.Service
{
    public class ProductService : IProductService
    {

        private readonly IBaseService _baseService;

        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> GetAllActiveBundles()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/product/GetAllActiveBundles"
            });
        }

        public async Task<ResponseDto?> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/product/GetProducts"
            });
        }
    }
}
