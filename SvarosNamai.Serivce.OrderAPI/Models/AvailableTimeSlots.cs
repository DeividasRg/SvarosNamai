using System;
using System.ComponentModel.DataAnnotations;

namespace SvarosNamai.Serivce.OrderAPI.Models
{
    public class AvailableTimeSlots
    {
        public string DayName { get; set; }
        [Key]
        public DateOnly DayDate {  get; set; }
        public int OpenSlots { get; set; }
        public int OrderCount { get; set; }
        public int AvailableSlots { get; set; } 

    }
}
