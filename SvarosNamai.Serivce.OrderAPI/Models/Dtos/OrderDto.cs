using System;
using System.ComponentModel.DataAnnotations;

namespace SvarosNamai.Serivce.OrderAPI.Models
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        [Range(1,1000)]
        public int HouseNo { get; set; }
        public int? ApartmentNo { get; set; }
        public string? HouseLetter { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? CompanyNumber { get; set; }
        public string? CompanyName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Hour { get; set; }
        public DateOnly Date {  get; set; }
        public int Status { get; set; }
        public DateTime CreationDate { get; set; }
        public double SquareFoot { get; set; }
        public bool IsCompany { get; set; } = false;
        public double Price { get; set; }

    }
}
