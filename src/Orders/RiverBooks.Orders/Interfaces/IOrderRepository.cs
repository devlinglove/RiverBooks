using RiverBooks.Orders.Domain;

namespace RiverBooks.Orders.Interfaces;

internal interface IOrderRepository
{
	Task<List<Order>> ListOrderAsync();
	Task AddAsync(Order order);

	Task SaveChangesAsync();

}

