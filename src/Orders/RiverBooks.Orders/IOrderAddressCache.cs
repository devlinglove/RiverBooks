using Ardalis.Result;


namespace RiverBooks.Orders
{
	internal interface IOrderAddressCache
	{
		//Task<Result<OrderAddress>> GetByIdAsync(Guid addressId);
		Task<Result> StoreAsync(OrderAddress orderAddress);
	}

}
