using AutoMapper;
using Newtonsoft.Json;
using SvarosNamai.Serivce.OrderAPI.Service.IService;
using SvarosNamai.Service.OrderAPI.Models.Dtos;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SvarosNamai.Serivce.OrderAPI.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private IMapper _mapper;
        private IErrorLogger _error;

        public InvoiceService(IHttpClientFactory httpClientFactory, IMapper mapper, IErrorLogger error)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
            _error = error;
        }


        public async Task<ResponseDto> GenerateInvoice(OrderForInvoiceDto order)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Invoice");
                var json = JsonConvert.SerializeObject(order);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"/api/invoice/GenerateInvoice", content);
                var apiContent = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                if (resp.IsSuccess)
                {
                    return resp;
                }
                else
                {
                    throw new Exception($"{resp.Message}");
                }
            }
            catch (Exception ex) 
            {
                _error.LogError(ex.Message);
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };

            }
        }
    }
}
