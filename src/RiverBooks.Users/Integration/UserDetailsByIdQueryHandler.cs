﻿using Ardalis.Result;
using MediatR;
using RiverBooks.Users.Contracts;
using RiverBooks.Users.UseCases.User.GetById;

namespace RiverBooks.Users.Integration
{
	internal class UserDetailsByIdQueryHandler : IRequestHandler<UserDetailsByIdQuery, Result<UserDetails>>
	{
		private readonly IMediator _mediator;

		public UserDetailsByIdQueryHandler(IMediator mediator)
		{
			_mediator = mediator;
		}
		public async Task<Result<UserDetails>> Handle(UserDetailsByIdQuery request, CancellationToken cancellationToken)
		{
			var query = new GetUserByIdQuery(request.UserId);

			var result = await _mediator.Send(query);

			return result.Map(u => new UserDetails(u.UserId, u.EmailAddress));
		}
	}
}
