using AutoMapper;
using Newtonsoft.Json;
using SvarosNamai.Serivce.OrderAPI.Service.IService;
using SvarosNamai.Service.OrderAPI.Models.Dtos;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SvarosNamai.Serivce.OrderAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private IMapper _mapper;

        public ProductService(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }


        public async Task<BundleDto> GetBundle(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Product");
                var response = await client.GetAsync($"/api/product/GetBundle/{id}");
                var apiContent = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                if (resp.IsSuccess)
                {
                    BundleDto bundle = JsonConvert.DeserializeObject<BundleDto>(Convert.ToString(resp.Result));
                    return bundle;
                }
                else
                {
                    throw new Exception($"{resp.Message}");
                }
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.ToString());
            }
            return new BundleDto();
        }
    }
}
