using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SvarosNamai.Serivce.EmailAPI.Service.IService
{
    public interface IErrorLogger
    {
        Task<IActionResult> LogError(string log);
    }
}
