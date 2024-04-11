using Newtonsoft.Json.Linq;
using SvarosNamai.Web.Service.IService;
using SvarosNamai.Web.Utility;

namespace SvarosNamai.Web.Service
{
    public class TokenProvider : ITokenProvider
    {
        public readonly IHttpContextAccessor _context;
        public TokenProvider(IHttpContextAccessor context)
        {
            _context = context;
        }



        public void ClearToken()
        {
            _context.HttpContext?.Response.Cookies.Delete(SD.TokenCookie);
        }

        public string? GetToken()
        {
            string? token = null;
            bool? hasToken = _context.HttpContext?.Request.Cookies.TryGetValue(SD.TokenCookie, out token);
            return hasToken is true ? token : null;
        }

        public void SetToken(string token)
        {
            _context.HttpContext?.Response.Cookies.Append(SD.TokenCookie, token);
        }
    }
}