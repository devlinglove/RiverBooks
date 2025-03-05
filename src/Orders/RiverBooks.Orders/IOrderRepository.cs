namespace RiverBooks.Orders;

internal interface IOrderRepository
{
	Task<List<Order>> ListOrderAsync();
	Task AddAsync(Order order);

	Task SaveChangesAsync();

}

