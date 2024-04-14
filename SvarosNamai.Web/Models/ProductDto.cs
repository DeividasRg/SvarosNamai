using System.ComponentModel.DataAnnotations;
using System;

namespace SvarosNamai.Web.Models
{
    public class ProductDto
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
