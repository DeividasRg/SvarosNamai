using SvarosNamai.Service.AuthAPI.Models.Dtos;
using System.Threading.Tasks;

namespace SvarosNamai.Service.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
