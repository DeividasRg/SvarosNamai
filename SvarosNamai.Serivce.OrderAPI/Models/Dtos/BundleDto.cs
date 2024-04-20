using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SvarosNamai.Service.OrderAPI.Models.Dtos
{
    public class BundleDto
    {
        public int BundleId { get; set; }
        public string BundleName { get; set; }
        public double HourPrice { get; set; }
        public double? Discount { get; set; }
    }
}
