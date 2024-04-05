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
        public int BundleId { get; set; }
        public int ProductId { get; set; }
    }
}
