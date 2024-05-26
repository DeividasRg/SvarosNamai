using System;
using System.ComponentModel.DataAnnotations;

namespace SvarosNamai.Serivce.OrderAPI.Models
{
    public class Reservations
    {
        [Key]
        public int ReservationId { get; set; }
        public DateOnly Date {  get; set; }
        public bool IsActive { get; set; } = true;
    }
}
