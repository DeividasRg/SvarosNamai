﻿namespace SvarosNamai.Web.Models
{
    public class OrderDetailDto
    {
        public OrderDto Order { get; set; }
        public IEnumerable<OrderLineDto> Lines { get; set; }
    }
}
