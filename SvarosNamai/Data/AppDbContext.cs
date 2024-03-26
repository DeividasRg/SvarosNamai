using Microsoft.EntityFrameworkCore;
using SvarosNamai.Service.ProductAPI.Models;

namespace SvarosNamai.Service.ProductAPI.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		public DbSet<Bundle> Bundles { get; set; }
		public DbSet<SvarosNamai.Service.ProductAPI.Models.Service> Services { get; set; }
		public DbSet<ServiceBundle> ServiceBundles { get; set; }
	}
}
