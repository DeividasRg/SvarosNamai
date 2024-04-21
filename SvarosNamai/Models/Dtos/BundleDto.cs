﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SvarosNamai.Service.ProductAPI.Models.Dtos
{
    public class BundleDto
    {
        public int BundleId { get; set; }
        public string BundleName { get; set; }
        public double HourPrice { get; set; }
     }
}
