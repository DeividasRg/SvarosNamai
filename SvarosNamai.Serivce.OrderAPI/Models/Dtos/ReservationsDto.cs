using System;

namespace SvarosNamai.Serivce.OrderAPI.Models.Dtos
{
    public class ReservationsDto
    {
        public DateOnly Date {  get; set; }
        public int Hour { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
