using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SvarosNamai.Service.ProductAPI.Models
{
    public class Bundle
    {
        [Key]
        public int BundleId { get; set; }
        [Required]
        public string BundleName { get; set; }
        [Range(1,100)]
        public double Price { get; set; }
        public double? Discount { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        
        public IEnumerable<Product> Products { get; set;}
    }
}
