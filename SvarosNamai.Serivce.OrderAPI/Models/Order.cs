using SvarosNamai.Serivce.OrderAPI.Utility;
using System;
using System.ComponentModel.DataAnnotations;

namespace SvarosNamai.Serivce.OrderAPI.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        [Range(1,1000)]
        public int HouseNo { get; set; }
        public string? HouseLetter { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName {  get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateOnly OrderDate { get; set; }
        public int? Status { get; set; } = OrderStatusses.Status_Pending;
    }
}
