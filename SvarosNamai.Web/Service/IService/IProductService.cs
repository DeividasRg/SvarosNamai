using SvarosNamai.Web.Models;

namespace SvarosNamai.Web.Service.IService
{
    public interface IProductService
    {
        Task<ResponseDto?> GetAllProductsAsync();
    }
}
