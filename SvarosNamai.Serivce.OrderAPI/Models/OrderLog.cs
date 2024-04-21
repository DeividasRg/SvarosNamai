using System;
using System.ComponentModel.DataAnnotations;

namespace SvarosNamai.Serivce.OrderAPI.Models
{
    public class OrderLog
    {
        [Key]
        public int OrderLogId { get; set; }
        public int OrderId {  get; set; }
        public int OrderStatus { get; set; }
        public string Email {  get; set; }
        public DateTime Time {  get; set; }
    }
}
