using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using SvarosNamai.Service.EmailAPI.Models.Dtos;

namespace SvarosNamai.Service.EmailAPI.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailAPIController : ControllerBase
    {
        private readonly ISendGridClient _sendgrid;
        private readonly ResponseDto _response;

        public EmailAPIController(ISendGridClient sendgrid)
        {
            _sendgrid = sendgrid;
            _response = new ResponseDto();
        }


        [HttpPost("SendEmail")]
        public async Task<ResponseDto> SendEmail()
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("deividasrg@gmail.com", "Deividas Ragauskas"),
                Subject = "Uzsakymo patvirtinimas",
                PlainTextContent = "random tekstas"
            };

            msg.AddTo(new EmailAddress("deividasrg@gmail.com", "Deividas gavejas"));
            var response = await _sendgrid.SendEmailAsync(msg);

            _response.IsSuccess = response.IsSuccessStatusCode;

            return _response;

            
        }
    }
}
