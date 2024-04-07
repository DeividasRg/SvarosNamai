using Newtonsoft.Json;
using SvarosNamai.Serivce.EmailAPI.Service.IService;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SvarosNamai.Serivce.EmailAPI.Service
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
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
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.ToString());
            }
            return new BundleDto();
        }
    }
}
