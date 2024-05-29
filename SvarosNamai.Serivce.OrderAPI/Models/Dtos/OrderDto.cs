using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SvarosNamai.Serivce.OrderAPI.Models.Dtos
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string HouseNo { get; set; }
        public int? ApartmentNo { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? CompanyNumber { get; set; }
        public string? CompanyName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateOnly Date { get; set; }
        public int Status { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now.RoundToNearestSecond();
        public double SquareMeters { get; set; }
        public bool IsCompany { get; set; } = false;
        public double Price { get; set; }
        public int BundleId { get; set; }
        public string? ProductId { get; set; }
        public IEnumerable<string>? DateStrings { get; set; }
    }

    public static class DateTimeExtensions
    {
        public static DateTime RoundToNearestSecond(this DateTime dateTime)
        {
            return new DateTime(dateTime.Ticks - (dateTime.Ticks % TimeSpan.TicksPerSecond), dateTime.Kind);
        }
    }
}
