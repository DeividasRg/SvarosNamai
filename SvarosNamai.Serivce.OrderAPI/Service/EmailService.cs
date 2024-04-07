using AutoMapper;
using Newtonsoft.Json;
using SvarosNamai.Serivce.OrderAPI.Service.IService;
using SvarosNamai.Service.OrderAPI.Models.Dtos;
using System;
using System.Net.Http;
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

        public EmailService(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
            _response = new ResponseDto();
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
                    _response.Message = result.Message;
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
