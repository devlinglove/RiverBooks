using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users;

public partial class UserDbContext : IdentityDbContext<ApplicationUser>
{
	private readonly IDomainEventDispatcher _dispatcher;

	public UserDbContext(
	  DbContextOptions<UserDbContext> options,
	  IDomainEventDispatcher dispatcher
	  ) : base(options)
  {
		_dispatcher = dispatcher;
	}

  public DbSet<ApplicationUser> ApplicationUsers { get; set; }
  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.HasDefaultSchema("Users");
    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    
    base.OnModelCreating(builder);
  }

  protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
  {
    configurationBuilder.Properties<decimal>().HavePrecision(18, 6);

  }

	public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

		if (_dispatcher is null) return result;

		var entitiesWithEvents = ChangeTracker.Entries<IHaveDomainEvents>()
		.Select(e => e.Entity)
		.Where(e => e.DomainEvents.Any()).ToArray();

		await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

		return result;
	}

}
