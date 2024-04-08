using Microsoft.AspNetCore.Mvc;
using SvarosNamai.Service.OrderAPI.Models.Dtos;
using System.Threading.Tasks;

namespace SvarosNamai.Serivce.OrderAPI.Service.IService
{
    public interface IErrorLogger
    {
        Task<IActionResult> LogError(string log);
    }
}
