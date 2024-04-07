

using Microsoft.EntityFrameworkCore;

namespace SvarosNamai.Service.OrderAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        
        public DbSet<Order> Orders {  get; set; }





    }
}
