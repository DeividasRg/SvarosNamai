namespace SvarosNamai.Web.Models
{
    public class RegistrationRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Role {  get; set; }
    }
}
