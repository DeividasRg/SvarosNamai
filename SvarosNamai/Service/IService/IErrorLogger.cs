using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SvarosNamai.Serivce.ProductAPI.Service.IService
{
    public interface IErrorLogger
    {
        Task<IActionResult> LogError(string log);
    }
}
