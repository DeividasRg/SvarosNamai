using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SvarosNamai.Service.ProductAPI.Models
{
    public class BundleToAddDto
    {
        public string BundleName { get; set; }
        [Range(1,100)]
        public double Price { get; set; }
       
    }
}
