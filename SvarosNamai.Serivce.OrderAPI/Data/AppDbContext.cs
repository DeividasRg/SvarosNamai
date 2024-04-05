using Microsoft.EntityFrameworkCore;
using SvarosNamai.Service.OrderAPI.Models;

namespace SvarosNamai.Service.OrderAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


    }
}
