using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using SvarosNamai.Serivce.EmailAPI.Service.IService;
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
        private readonly IErrorLogger _error;

        public EmailAPIController(ISendGridClient sendgrid,AppDbContext db, IErrorLogger error)
        {
            _sendgrid = sendgrid;
            _response = new ResponseDto();
            _db = db;
            _error = error;
        }

        [HttpPost("SendCompleteEmail")]
        public async Task<ResponseDto> SendCompleteEmail(ConfirmationEmailDto info)
        {
            try
            {
                if (info.pdfFile == null || info.pdfFile.Length <= 0 || info == null)
                {
                    throw new Exception("Incorrect info");
                }

                string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Invoices");
                if(!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }


                var filepath = Path.Combine(directoryPath, $"{info.OrderId}_{info.Address}" + ".pdf");
                using (var stream = new MemoryStream(info.pdfFile))
                {
                    using (var fileStream = new FileStream(filepath,FileMode.Create))
                    {
                        await stream.CopyToAsync(fileStream);
                    }
                }

                    string message = "Užsakymas įvkdytas, prisegame sąskaitą faktūrą.";

                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("deividasrg@gmail.com", "Deividas Ragauskas"),
                    Subject = $"Jūsų užsakymas {info.OrderId} įvykdytas",
                    PlainTextContent = message
                };

                string base64Content = Convert.ToBase64String(info.pdfFile);

                msg.AddAttachment($"Sąskaita faktūrą Nr_{info.OrderId}.pdf", base64Content);
                msg.AddTo(new EmailAddress($"{info.Email}", $"{info.Name} {info.LastName}"));
                var response = await _sendgrid.SendEmailAsync(msg);

                _response.IsSuccess = response.IsSuccessStatusCode;

                if(!response.IsSuccessStatusCode)
                {
                    _error.LogError(response.Body.ToString());
                    _response.IsSuccess = false;
                    EmailLog logfalse = new EmailLog()
                    {
                        Email = info.Email,
                        Content = message,
                        WasSent = _response.IsSuccess,
                        HadAttachment = (msg.Attachments.Count > 0) ? true : false,
                        Time = DateTime.Now
                    };
                    _db.EmailLogs.Add(logfalse);
                    await _db.SaveChangesAsync();
                    return _response;
                }



                EmailLog log = new EmailLog()
                {
                    Email = info.Email,
                    Content = message,
                    WasSent = _response.IsSuccess,
                    HadAttachment = (msg.Attachments.Count > 0) ? true : false,
                    Time = DateTime.Now
                };
                _db.EmailLogs.Add(log);
                await _db.SaveChangesAsync();

            }
            catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _error.LogError(_response.Message);
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

                if (!response.IsSuccessStatusCode)
                {
                    _error.LogError(response.Body.ToString());
                }



                EmailLog log = new EmailLog()
                {
                    Email = info.Email,
                    Content = message,
                    WasSent = _response.IsSuccess,
                    HadAttachment = (msg.Attachments.Count > 0) ? true : false,
                    Time = DateTime.Now
                };
                _db.EmailLogs.Add(log);
                await _db.SaveChangesAsync();
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
