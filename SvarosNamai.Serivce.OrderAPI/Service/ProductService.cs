using AutoMapper;
using Newtonsoft.Json;
using SvarosNamai.Serivce.OrderAPI.Service.IService;
using SvarosNamai.Service.OrderAPI.Models.Dtos;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace SvarosNamai.Serivce.OrderAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private IMapper _mapper;
        private IErrorLogger _error;
        private ResponseDto _response;

        public ProductService(IHttpClientFactory httpClientFactory, IMapper mapper, IErrorLogger error)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
            _error = error;
            _response = new ResponseDto();
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
                _error.LogError(ex.Message);
                throw new Exception(ex.ToString());
            }
        }

        public async Task<ResponseDto> GetProduct(int productId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Product");
                var response = await client.GetAsync($"/api/product/GetProduct/{productId}");
                var apiContent = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                if(resp.IsSuccess)
                {
                    return resp;
                }
                else
                {
                    throw new Exception("GetProduct didn't receive a positive response");
                }

            }
            catch(Exception ex)
            {
                _error.LogError(ex.Message);
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return _response;
            }
        }

        public async Task<ResponseDto> GetProductByName(string productName)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Product");
                var response = await client.GetAsync($"/api/product/GetProductByName/{productName}");
                var apiContent = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                if (resp.IsSuccess)
                {
                    return resp;
                }
                else
                {
                    throw new Exception("GetProductByName didn't receive a positive response");
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
    }
}
