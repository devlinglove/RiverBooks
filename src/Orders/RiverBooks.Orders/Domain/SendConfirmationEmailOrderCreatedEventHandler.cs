using MediatR;
using RiverBooks.EmailSending.Contracts;
using RiverBooks.Users.Contracts;

namespace RiverBooks.Orders.Domain
{
	//internal class SendConfirmationEmailOrderCreatedEventHandler 
	internal class SendConfirmationEmailOrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
	{
		private readonly IMediator _mediator;

		public SendConfirmationEmailOrderCreatedEventHandler(IMediator mediator)
		{
			_mediator = mediator;
		}
		public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
		{

			var userId = notification.Order.UserId;

			var query = new UserDetailsByIdQuery(userId);
			var result = await _mediator.Send(query);
			if (!result.IsSuccess)
			{
				// TODO: Log the error
				return;
			}
			
			
			var command = new EmailSendingCommand()
			{
				To = result.Value.EmailAddress,
				From = "noreply@test.com",
				Subject = "Your RiverBooks Purchase",
				Body = $"You bought {notification.Order.OrderItems.Count} items."
			};

			Guid emailId = await _mediator.Send(command);

			// TODO: Do something with emailId
			
		}
	}
}
