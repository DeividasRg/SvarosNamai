using SvarosNamai.Web.Models;

namespace SvarosNamai.Web.Service.IService
{
	public interface IAuthService
	{
		Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
	}
}
