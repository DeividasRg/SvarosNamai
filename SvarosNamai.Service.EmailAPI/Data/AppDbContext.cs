

using Microsoft.EntityFrameworkCore;
using SvarosNamai.Service.EmailAPI.Models;

namespace SvarosNamai.Service.OrderAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }



        public DbSet<EmailLog> EmailLogs { get; set; }



    }
}
