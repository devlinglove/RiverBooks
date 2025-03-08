using MediatR;
using Microsoft.Extensions.Logging;
using RiverBooks.Orders.Interfaces;
using RiverBooks.Users.Contracts;
using RiverBooks.Orders.Infrastructure;
using RiverBooks.Orders.Domain;



namespace RiverBooks.Orders.Integrations
{
	internal class AddressCacheUpdatingNewUserAddressHandler : INotificationHandler<NewUserAddressAddedIntegrationEvent>
	{
		private readonly IOrderAddressCache _orderAddressCache;
		private readonly ILogger<AddressCacheUpdatingNewUserAddressHandler> _logger;

		public AddressCacheUpdatingNewUserAddressHandler(
			IOrderAddressCache orderAddressCache,
			ILogger<AddressCacheUpdatingNewUserAddressHandler> logger
		)
		{
			_orderAddressCache = orderAddressCache;
			_logger = logger;
		}
		public async Task Handle(NewUserAddressAddedIntegrationEvent notification, CancellationToken cancellationToken)
		{
			var orderAddress = new OrderAddress(notification.Details.AddressId,
	 new Address(notification.Details.Street1,
	   notification.Details.Street2,
	   notification.Details.City,
	   notification.Details.State,
	   notification.Details.PostalCode,
	   notification.Details.Country));

			await _orderAddressCache.StoreAsync(orderAddress);

			_logger.LogInformation("Cache updated with new address {address}", orderAddress);
		}
	}
}
