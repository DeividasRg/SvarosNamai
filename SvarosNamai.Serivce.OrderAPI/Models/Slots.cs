using System.ComponentModel.DataAnnotations;

namespace SvarosNamai.Serivce.OrderAPI.Models
{
    public class Slots
    {
        [Key]
        public string Weekday { get; set; }
        public int OpenSlots { get; set; }
    }
}
