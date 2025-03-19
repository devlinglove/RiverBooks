using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RiverBooks.Orders.Domain;
using RiverBooks.SharedKernel;
using System.Reflection;

namespace RiverBooks.Orders.Infrastructure.Data
{
	internal class OrderDbContext : DbContext
	{
		private readonly IDomainEventDispatcher _dispatcher;

		public OrderDbContext(
			DbContextOptions<OrderDbContext> options,
			IDomainEventDispatcher dispatcher) : base(options)
		{
			_dispatcher = dispatcher;
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

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

			// ignore events if no dispatcher provided
			if (_dispatcher == null) return result;

			// dispatch events only if save was successful
			var entitiesWithEvents = ChangeTracker.Entries<IHaveDomainEvents>()
				.Select(e => e.Entity)
				.Where(e => e.DomainEvents.Any())
			.ToArray();

			await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

			return result;
		}




	}
}
