using Microsoft.EntityFrameworkCore;
using SvarosNamai.Serivce.OrderAPI.Models;
using SvarosNamai.Service.OrderAPI.Models;

namespace SvarosNamai.Service.OrderAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<Order> Orders {  get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Reservations> Reservations { get; set; } 
        public DbSet<OrderLog> OrderLogs { get; set; }  




    }
}
