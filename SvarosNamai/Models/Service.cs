using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SvarosNamai.Service.ProductAPI.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }

        public IEnumerable<ServiceBundle> Bundles { get; set; }
    }
}
