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

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<BundleDto> GetBundle(int id)
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync($"/api/GetBundle/{id}");
            var apiContent = await response.Content.ReadAsStringAsync(); ;
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if(resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<BundleDto>(Convert.ToString(resp.Result));
            }
            return new BundleDto();
        }
    }
}
