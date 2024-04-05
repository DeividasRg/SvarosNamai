using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SvarosNamai.Service.ProductAPI.Models
{
    public class ProductBundle
    {
        [Key]
        public int ProductBundleId { get; set; }
        [ForeignKey("BundleId")]
        public Bundle Bundle { get; set; }
        public Product Product { get; set; }
    }
}
