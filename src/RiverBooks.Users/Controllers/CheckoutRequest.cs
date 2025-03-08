namespace RiverBooks.Users.Controllers
{
	public record CheckoutRequest(Guid shippingAddressId, Guid billingAddressId);
	
}
