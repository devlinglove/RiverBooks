using MediatR;

namespace RiverBooks.Users.Contracts
{
	public record NewUserAddressAddedIntegrationEvent(UserAddressDetails Details): IntegrationEventBase;



	public abstract record IntegrationEventBase : INotification
	{
		public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.UtcNow;
	}
}
