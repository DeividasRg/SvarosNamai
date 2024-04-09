namespace SvarosNamai.Service.InvoiceAPI.Models.Dtos
{
    public class OrderLinesForInvoiceDto
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public double? Price { get; set; }
    }
}
