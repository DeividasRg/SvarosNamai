using System;
using System.ComponentModel.DataAnnotations;

namespace SvarosNamai.Service.InvoiceAPI.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceID { get; set; }
        public int OrderId { get; set; }
        public string InvoiceName { get; set; }
        public DateTime Created {  get; set; } = DateTime.Now;
        public string Path {  get; set; } 
    }
}
