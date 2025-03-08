namespace RiverBooks.Users;

public partial class UserDbContext
{
	public interface IDomainEventDispatcher
	{
		Task DispatchAndClearEvents(IEnumerable<IHaveDomainEvents> entitiesWithEvents);
	}

}
