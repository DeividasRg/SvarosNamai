using System;

namespace SvarosNamai.Serivce.OrderAPI.Models.Dtos
{
    public class ReservationsIntervalDto
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
