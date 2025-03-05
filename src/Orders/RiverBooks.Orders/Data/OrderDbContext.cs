using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace RiverBooks.Orders.Data
{
	internal class OrderDbContext:DbContext
	{
		public OrderDbContext(DbContextOptions<OrderDbContext> options): base(options)
		{
			
		}

		public DbSet<Order> Orders { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("Orders");
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}

		protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
		{
			configurationBuilder.Properties<decimal>().HavePrecision(18, 6);
		}
	}
}
