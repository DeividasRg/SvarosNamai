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
        public double HourPrice { get; set; }
       
    }
}
