using System.ComponentModel.DataAnnotations.Schema;

namespace SvarosNamai.Web.Models
{
    public class OrderLinesForInvoiceDto
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public double? Price { get; set; }
    }
}
