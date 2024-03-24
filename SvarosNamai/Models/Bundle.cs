﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SvarosNamai.Service.ProductAPI.Models
{
    public class Bundle
    {
        [Key]
        public string BundleId { get; set; }
        public string BundleName {  get; set; }
        public double Price { get; set; }
        public double? Discount { get; set; }
        public string Description { get; set; } 
        
        public IEnumerable<Product> Products { get; set;}
    }
}
