using SvarosNamai.Web.Models;

namespace SvarosNamai.Web.Service.IService
{
    public interface IOrderService
    {
        Task<ResponseDto?> GetAllOrdersAsync();
    }
}
