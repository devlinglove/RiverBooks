using RiverBooks.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Orders.Domain
{
	internal class OrderCreatedEvent:DomainEventBase
	{
		public Order Order { get; }

		public OrderCreatedEvent(Order order)
		{
			Order = order;
		}
	}
}
