using System.Collections;
using System.Collections.Generic;

namespace SvarosNamai.Service.InvoiceAPI.Models.Dtos
{
    public class OrderForInvoiceDto
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Street {  get; set; }
        public int HouseNo { get; set; }
        public string? HouseLetter { get; set; }
        public int? ApartmentNo { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Status {  get; set; }
        public IEnumerable<OrderLinesForInvoiceDto> Lines { get; set; }
    }
}
