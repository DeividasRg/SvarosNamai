using SvarosNamai.Service.OrderAPI.Models.Dtos;
using System.Threading.Tasks;

namespace SvarosNamai.Serivce.OrderAPI.Service.IService
{
    public interface IInvoiceService
    {
        Task<ResponseDto> GenerateInvoice(OrderForInvoiceDto order);
    }
}
