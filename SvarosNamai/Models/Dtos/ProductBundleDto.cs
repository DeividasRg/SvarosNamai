using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SvarosNamai.Service.ProductAPI.Models.Dtos
{
    public class ProductBundleDto
    {
        public int BundleId { get; set; }
        public List<int> ProductIds { get; set; }
    }
}
