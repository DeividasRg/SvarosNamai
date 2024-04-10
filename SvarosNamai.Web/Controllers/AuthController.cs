using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SvarosNamai.Web.Models;
using SvarosNamai.Web.Service.IService;


namespace SvarosNamai.Web.Controllers
{
	public class AuthController : Controller
	{

		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}


		[HttpGet]
		public IActionResult Login()
		{
			LoginRequestDto loginRequest = new();
				return View(loginRequest);
		}

        public IActionResult Logout()
        {
			return View();
        }
    }
}
