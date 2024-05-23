using SvarosNamai.Serivce.OrderAPI.Utility;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string HouseNo { get; set; }
        public int? ApartmentNo { get; set; }
        public string? Name { get; set; }
        public string? LastName {  get; set; }
        public string? CompanyNumber { get; set; }
        public string? CompanyName { get; set; }
        public bool IsCompany { get; set; } = false;
        [Required]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public double Price { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        [ForeignKey("ReservationId")]
        public virtual Reservations Reservation { get; set; }
        public int Status { get; set; } = OrderStatusses.Status_Pending;
        public double SquareMeters { get; set; }
    }
}
