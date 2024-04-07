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

        [HttpPost("SendCompleteEmail")]
        public async Task<ResponseDto> SendCompleteEmail([FromForm] ConfirmationEmailDto info, [FromForm] IFormFile pdfFile)
        {
            try
            {
                if(pdfFile == null || pdfFile.Length <= 0 || info == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Incorrect info";
                    return _response;
                }

                var filepath = Path.Combine("pdf_files", Guid.NewGuid().ToString() + ".pdf");
                using (var stream = new FileStream(filepath,FileMode.Create))
                {
                    await pdfFile.CopyToAsync(stream);
                }

                string message = "Užsakymas įvkdytas, prisegame sąskaitą faktūrą.";

                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("deividasrg@gmail.com", "Deividas Ragauskas"),
                    Subject = $"Jūsų užsakymas {info.OrderId} įvykdytas",
                    PlainTextContent = message
                };

                msg.AddAttachment(filepath, "order.pdf");
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
            catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
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
                    case -1:
                        message = $"Jūsų užsakymas Nr.{info.OrderId} atšauktas";
                        subject = $"Užsakymas {info.OrderId} atšauktas";
                        break;
                    default:
                        throw new Exception();
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
