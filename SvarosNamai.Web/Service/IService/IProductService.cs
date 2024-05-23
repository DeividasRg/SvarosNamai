using SvarosNamai.Web.Models;

namespace SvarosNamai.Web.Service.IService
{
    public interface IProductService
    {
        Task<ResponseDto?> GetAllProductsAsync();
        Task<ResponseDto?> GetAllActiveBundles();
        Task<ResponseDto?> GetBundle(int? bundleId);
        Task<ResponseDto?> GetProduct(int? productId);   
    }
}
