using SvarosNamai.Web.Models;

namespace SvarosNamai.Web
{
    public class OrderToAddDto
    {
        public OrderDto Order {  get; set; }
        public int BundleId { get; set; }
    }
}
