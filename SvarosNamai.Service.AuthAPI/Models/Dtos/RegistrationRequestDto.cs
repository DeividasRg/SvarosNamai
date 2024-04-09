namespace SvarosNamai.Service.AuthAPI.Models.Dtos
{
    public class RegistrationRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Role {  get; set; }
    }
}
