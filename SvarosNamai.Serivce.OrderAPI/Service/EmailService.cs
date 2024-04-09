using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using SvarosNamai.Serivce.OrderAPI.Service.IService;
using SvarosNamai.Service.OrderAPI.Models.Dtos;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace SvarosNamai.Serivce.OrderAPI.Service
{
    public class EmailService : IEmailService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private IMapper _mapper;
        private readonly ResponseDto _response;
        private readonly IErrorLogger _error;

        public EmailService(IHttpClientFactory httpClientFactory, IMapper mapper, IErrorLogger error)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
            _response = new ResponseDto();
            _error = error;
        }


        public async Task<ResponseDto> SendConfirmationEmail(ConfirmationEmailDto info)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Email");
                var json = JsonConvert.SerializeObject(info);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"/api/email/SendConfirmationEmail", content);
                var apiContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

                if (!result.IsSuccess)
                {
                    throw new Exception($"{result.Message}");
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _error.LogError(_response.Message);
            }
            return _response;

        }

        public async Task<ResponseDto> SendCompleteEmail(ConfirmationEmailDto info)
        {
            try
            {

                if (info == null)
                {
                    throw new Exception("info not provided");
                }
                
                
                var client = _httpClientFactory.CreateClient("Email");


                var json = JsonConvert.SerializeObject(info);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"/api/email/SendCompleteEmail", content);
                    var apiContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

                    if (!result.IsSuccess)
                    {
                    throw new Exception($"{result.Message}");
                    }
                

            }
            catch(Exception ex) 
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _error.LogError(_response.Message);
            }
            return _response;
            
        }

        public async Task<ResponseDto> GetInvoice(int orderId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Email");
                var response = await client.GetAsync($"/api/email/GetInvoice/{orderId}");
                var apiContent = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                if (resp.IsSuccess)
                {
                    _response.Result = resp.Result;
                }
                else
                {
                    throw new Exception($"{resp.Message}");
                }
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _error.LogError(_response.Message);
            }
            return _response;
        }
    }
}
