using System;
using System.ComponentModel.DataAnnotations;

namespace SvarosNamai.Web.Models
{
    public class ReservationsDto
    {
        public DateOnly Date {  get; set; }
        public bool IsActive { get; set; } = true;
    }
}
