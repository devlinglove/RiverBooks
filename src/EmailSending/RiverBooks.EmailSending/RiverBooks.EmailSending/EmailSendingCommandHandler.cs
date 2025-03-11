using Ardalis.Result;
using MediatR;
using RiverBooks.Users.Contracts;

namespace RiverBooks.EmailSending
{

	internal class EmailSendingCommandHandler : IRequestHandler<EmailSendingCommand, Result<Guid>>
	{
		private readonly ISenderEmail _senderEmail;

		public EmailSendingCommandHandler(ISenderEmail senderEmail)
		{
			_senderEmail = senderEmail;
		}
		public async Task<Result<Guid>> Handle(EmailSendingCommand request, CancellationToken cancellationToken)
		{
			await _senderEmail.SendEmailAsync(request.To, request.From, request.Subject, request.Body);

			return Guid.Empty;
		}
	}


}
