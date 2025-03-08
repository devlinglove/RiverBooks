using Ardalis.Result;
using RiverBooks.Orders.Infrastructure;


namespace RiverBooks.Orders.Interfaces
{
	internal interface IOrderAddressCache
	{
		Task<Result<OrderAddress>> GetByIdAsync(Guid addressId);
		Task<Result> StoreAsync(OrderAddress orderAddress);
	}

}
