using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SvarosNamai.Serivce.OrderAPI.Models
{
    public class OrderLine
    {
        [Key]
        public int OrderLineId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        public string ProductName { get; set; }
        public double? Price { get; set; } = 0;
    }
}
