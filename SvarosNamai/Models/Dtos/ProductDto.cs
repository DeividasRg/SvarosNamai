using System.ComponentModel.DataAnnotations;
using System;

namespace SvarosNamai.Service.ProductAPI.Models.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
