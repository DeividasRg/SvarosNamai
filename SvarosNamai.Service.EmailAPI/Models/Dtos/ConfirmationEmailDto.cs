namespace SvarosNamai.Service.EmailAPI.Models.Dtos
{
    public class ConfirmationEmailDto
    {
        public string Email { get; set; }   
        public string Name { get; set; }
        public string LastName { get; set; }
        public int OrderId { get; set; }
        public int OrderStatus { get; set; }
        public DateOnly Date {  get; set; }
        public int Hour { get; set; }
        public string Address { get; set; }
        public byte[]? pdfFile { get; set; }
        public string? message {  get; set; }
    }
}
