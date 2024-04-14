using SvarosNamai.Service.OrderAPI.Models.Dtos;
using System.Threading.Tasks;

namespace SvarosNamai.Serivce.OrderAPI.Service.IService
{
    public interface IProductService
    {
        Task<BundleDto> GetBundle(int id);
        Task<ResponseDto> GetProduct(int productId);
        Task<ResponseDto> GetProductByName(string productName);
    }
}
