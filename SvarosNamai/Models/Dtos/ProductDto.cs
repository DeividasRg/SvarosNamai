using System.ComponentModel.DataAnnotations;
using System;

namespace SvarosNamai.Service.ProductAPI.Models.Dtos
{
    public class ProductDto
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price {  get; set; }
    }
}
