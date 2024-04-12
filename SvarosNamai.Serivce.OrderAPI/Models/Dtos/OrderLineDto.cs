using System.ComponentModel.DataAnnotations.Schema;

namespace SvarosNamai.Serivce.OrderAPI.Models.Dtos
{
    public class OrderLineDto
    {
        public int OrderLineId { get; set; }
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public double? Price { get; set; }
    }
}
