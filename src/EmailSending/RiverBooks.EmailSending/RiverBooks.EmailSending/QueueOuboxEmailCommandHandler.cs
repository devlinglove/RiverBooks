using Ardalis.Result;
using MediatR;
using RiverBooks.EmailSending.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.EmailSending
{
	internal class QueueOuboxEmailCommandHandler : IRequestHandler<EmailSendingCommand, Result<Guid>>
	{
		private readonly IMediator _mediator;
		private readonly IOutboxService _outboxService;

		public QueueOuboxEmailCommandHandler(
			IMediator mediator,
			IOutboxService outboxService
		)
		{
			_mediator = mediator;
			_outboxService = outboxService;
		}

		public async Task<Result<Guid>> Handle(EmailSendingCommand request, CancellationToken cancellationToken)
		{
			//await _senderEmail.SendEmailAsync(request.To, request.From, request.Subject, request.Body);

			var outBoxEntity = new EmailOutboxEntity
			{
				To = request.To,
				From = request.From,
				Subject = request.Subject,
				Body = request.Body
			};
			await _outboxService.QueEmailForSending(outBoxEntity);
			var sss = outBoxEntity.Id;

			return Guid.NewGuid();
			
		}
	}
}
