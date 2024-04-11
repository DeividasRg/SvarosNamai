using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SvarosNamai.Web.Models;
using SvarosNamai.Web.Service.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace SvarosNamai.Web.Controllers
{
	public class AuthController : Controller
	{

		private readonly IAuthService _authService;
		private readonly ITokenProvider _tokenProvider;

		public AuthController(IAuthService authService, ITokenProvider tokenProvider)
		{
			_authService = authService;
			_tokenProvider = tokenProvider;
		}


		[HttpGet]
		public IActionResult Login()
		{
			LoginRequestDto loginRequest = new();
				return View(loginRequest);
		}

		[HttpPost]	
		public async Task<IActionResult> Login(LoginRequestDto loginRequest)
		{
			ResponseDto responseDto = await _authService.LoginAsync(loginRequest);

			if(responseDto != null && responseDto.IsSuccess)
			{
				LoginResponseDto loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(responseDto.Result.ToString());
				await SignInUser(loginResponse);
				_tokenProvider.SetToken(loginResponse.Token);

				return RedirectToAction("Index","Home");
			}
			else
			{
				ModelState.AddModelError("CustomError", responseDto.Message);
				return View(loginRequest);
			}



		}

        public async Task<IActionResult> Logout()
        {
			await HttpContext.SignOutAsync();
			_tokenProvider.ClearToken();
			return RedirectToAction("Index", "Home");
        }

		private async Task SignInUser(LoginResponseDto model)
		{
			var handler = new JwtSecurityTokenHandler();

			var jwt = handler.ReadJwtToken(model.Token);

			var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

			identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));
            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));


            var principal = new ClaimsPrincipal(identity);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
		}
    }
}
