using Microsoft.AspNetCore.Identity;

namespace SvarosNamai.Service.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(IdentityUser identityUser);
    }
}
