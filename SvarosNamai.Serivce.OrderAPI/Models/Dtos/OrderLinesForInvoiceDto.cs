using System.ComponentModel.DataAnnotations.Schema;
using SvarosNamai.Serivce.OrderAPI.Models;

namespace SvarosNamai.Service.OrderAPI.Models.Dtos
{
    public class OrderLinesForInvoiceDto
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public double? Price { get; set; }
    }
}
