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
        public DbSet<Slots> Slots { get; set; }
        public DbSet<AvailableTimeSlots> AvailableTimeSlots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data
            modelBuilder.Entity<Slots>().HasData(
                new Slots { Weekday = "Monday", OpenSlots = 0 },
                new Slots { Weekday = "Tuesday", OpenSlots = 0 },
                new Slots { Weekday = "Wednesday", OpenSlots = 0 },
                new Slots { Weekday = "Thursday", OpenSlots = 0 },
                new Slots { Weekday = "Friday", OpenSlots = 0 },
                new Slots { Weekday = "Saturday", OpenSlots = 0 },
                new Slots { Weekday = "Sunday", OpenSlots = 0 }
            );
        }


    }
}
