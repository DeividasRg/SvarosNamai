using SvarosNamai.Web.Models;

namespace SvarosNamai.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
