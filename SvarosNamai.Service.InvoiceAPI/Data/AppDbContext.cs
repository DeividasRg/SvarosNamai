using Microsoft.EntityFrameworkCore;
using SvarosNamai.Service.InvoiceAPI.Models;

namespace SvarosNamai.Service.InvoiceAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<Invoice> Invoices {  get; set; }
  




    }
}
