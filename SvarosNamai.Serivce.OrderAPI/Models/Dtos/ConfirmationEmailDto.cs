namespace SvarosNamai.Service.OrderAPI.Models.Dtos
{
    public class ConfirmationEmailDto
    {
        public string Email { get; set; }   
        public string Name { get; set; }
        public string LastName { get; set; }
        public int OrderId { get; set; }
    }
}
