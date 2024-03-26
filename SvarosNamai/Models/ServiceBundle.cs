using SvarosNamai.Service.ProductAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SvarosNamai.Service.ProductAPI.Models
{
    public class ServiceBundle
    {
        [Key, Column(Order = 0)]
        public string BundleId { get; set; }

        [Key, Column(Order = 1)]
        public int ServiceId { get; set; }


        public Bundle Bundle { get; set; }

        public Service Service { get; set; }

    }
}
