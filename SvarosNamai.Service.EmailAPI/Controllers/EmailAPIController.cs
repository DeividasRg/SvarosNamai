using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using SvarosNamai.Service.EmailAPI.Models;
using SvarosNamai.Service.EmailAPI.Models.Dtos;
using SvarosNamai.Service.OrderAPI.Data;

namespace SvarosNamai.Service.EmailAPI.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailAPIController : ControllerBase
    {
        private readonly ISendGridClient _sendgrid;
        private readonly ResponseDto _response;
        private readonly AppDbContext _db;

        public EmailAPIController(ISendGridClient sendgrid,AppDbContext db)
        {
            _sendgrid = sendgrid;
            _response = new ResponseDto();
            _db = db;
        }


        [HttpPost("SendConfirmationEmail")]
        public async Task<ResponseDto> SendConfirmationEmail(ConfirmationEmailDto info)
        {
            try
            {


                string message;
                string subject;


                switch (info.OrderStatus)
                {
                        case 0:
                            message = $"Jūsų užsakymas Nr.{info.OrderId} pateiktas, laukite patvirtinimo";
                            subject = $"Užsakymas {info.OrderId} pateiktas";
                            break;
                        case 1:
                            message = $"Jūsų užsakymas Nr.{info.OrderId} patvirtintas adresu {info.Address}, {info.Date} dieną {info.Hour} valandą";
                        subject = $"Užsakymas {info.OrderId} patvirtinimas";
                        break;
                    default:
                        throw new Exception();
                        break;
                }


                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("deividasrg@gmail.com", "Deividas Ragauskas"),
                    Subject = subject,
                    PlainTextContent = message
                };
                

                msg.AddTo(new EmailAddress($"{info.Email}", $"{info.Name} {info.LastName}"));
                var response = await _sendgrid.SendEmailAsync(msg);

                _response.IsSuccess = response.IsSuccessStatusCode;



                EmailLog log = new EmailLog()
                {
                    Email = info.Email,
                    Content = message,
                    WasSent = _response.IsSuccess
                };
                _db.EmailLogs.Add(log);
                await _db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;    
                _response.Message = ex.Message;
            }

            return _response;

            
        }
    }
}
