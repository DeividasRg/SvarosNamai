using System.ComponentModel.DataAnnotations;
using System;

namespace SvarosNamai.Service.OrderAPI.Models.Dtos
{
    public class ProductDto
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
