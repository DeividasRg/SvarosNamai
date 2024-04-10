using SvarosNamai.Web.Models;
using SvarosNamai.Web.Service.IService;
using SvarosNamai.Web.Utility;

namespace SvarosNamai.Web.Service
{
	public class AuthService : IAuthService
	{

		public readonly IBaseService _baseService;

		public AuthService(IBaseService baseService)
		{
			_baseService = baseService;
		}


		public async Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
		{
			return await _baseService.SendAsync(new RequestDto ()
			{
				ApiType = SD.ApiType.POST,
				Data =  loginRequestDto,
				Url = SD.AuthAPIBase + "/api/auth/Login"
			});
		}
	}
}
