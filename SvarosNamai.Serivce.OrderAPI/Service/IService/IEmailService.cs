using Microsoft.AspNetCore.Http;
using SvarosNamai.Service.OrderAPI.Models.Dtos;
using System.Threading.Tasks;

namespace SvarosNamai.Serivce.OrderAPI.Service.IService
{
    public interface IEmailService
    {
        Task<ResponseDto> SendConfirmationEmail(ConfirmationEmailDto info);
        Task<ResponseDto> SendCompleteEmail(ConfirmationEmailDto info);
        Task<ResponseDto> GetInvoice(int orderId);
    }
}
