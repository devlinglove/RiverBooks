using Microsoft.EntityFrameworkCore;
using RiverBooks.Orders.Data;

namespace RiverBooks.Orders
{
	internal class EfOrderRepository : IOrderRepository
	{
		private readonly OrderDbContext _orderDbContext;

		public EfOrderRepository(OrderDbContext orderDbContext)
		{
			_orderDbContext = orderDbContext;
		}

		public async Task AddAsync(Order order)
		{
			await _orderDbContext.AddAsync(order);
		}

		public async Task<List<Order>> ListOrderAsync()
		{
			return await _orderDbContext.Orders.ToListAsync();
		}

		public async Task SaveChangesAsync()
		{
			await _orderDbContext.SaveChangesAsync();
		}
	}
}
