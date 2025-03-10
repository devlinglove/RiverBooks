﻿using Ardalis.Result;
using MediatR;
using RiverBooks.Users.DTOs;

namespace RiverBooks.Users.UseCases.Cart.ListItems
{
	public class ListCartItemsQueryHandler : IRequestHandler<ListCartItemsQuery, Result<List<CartItemDto>>>
	{
		private readonly IApplicationUserRepository _applicationUserRepository;

		public ListCartItemsQueryHandler(IApplicationUserRepository applicationUserRepository)
		{
			_applicationUserRepository = applicationUserRepository;
		}
		public async Task<Result<List<CartItemDto>>> Handle(ListCartItemsQuery request, CancellationToken cancellationToken)
		{
			var user = await _applicationUserRepository.GetUserWithCartByEmailAsync(request.emailAddress);

			if (user == null)
			{
				return Result.Unauthorized();
			}

			return user.CartItems.Select(item => new CartItemDto
			{
				BookId = item.BookId,
				Description = item.Description,
				Quantity = item.Quantity
			}).ToList();


		}
	}
}
