using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RiverBooks.Users.UserDbContext;

namespace RiverBooks.Users
{
	public class MediatRDomainEventDispatcher : IDomainEventDispatcher
	{
		private readonly IMediator _mediator;

		public MediatRDomainEventDispatcher(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task DispatchAndClearEvents(IEnumerable<IHaveDomainEvents> entitiesWithEvents)
		{
			foreach (var entity in entitiesWithEvents)
			{
				var events = entity.DomainEvents.ToArray();
				entity.ClearDomainEvents();
				foreach (var domainEvent in events)
				{
					await _mediator.Publish(domainEvent).ConfigureAwait(false);
				}
			}
		}
	}
}
