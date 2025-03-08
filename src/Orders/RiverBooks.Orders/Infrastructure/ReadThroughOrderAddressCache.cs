using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using RiverBooks.Orders.Interfaces;
using RiverBooks.Users.Contracts;
using StackExchange.Redis;
using RiverBooks.Orders.Domain;

namespace RiverBooks.Orders.Infrastructure
{
	internal class ReadThroughOrderAddressCache : IOrderAddressCache
	{
		private readonly ILogger<RedisOrderAddressCache> _logger;
		private readonly RedisOrderAddressCache _redisCache;
		private readonly IMediator _mediator;
		private readonly IDatabase _db;


		public ReadThroughOrderAddressCache(
			ILogger<RedisOrderAddressCache> logger,
			RedisOrderAddressCache redisCache,
			IMediator mediator

		)
		{
			var redis = ConnectionMultiplexer.Connect("localhost");
			_db = redis.GetDatabase();
			_logger = logger;
			_redisCache = redisCache;
			_mediator = mediator;
		}

		public async Task<Result<OrderAddress>> GetByIdAsync(Guid addressId)
		{
			var result = await _redisCache.GetByIdAsync(addressId);
			if (result.IsSuccess) return result;

			if (result.Status == ResultStatus.NotFound)
			{
				var query = new UserAddressDetailsByIdQuery(addressId);
				var queryResult = await _mediator.Send(query);

				if (queryResult.IsSuccess)
				{
					var dto = queryResult.Value;
					var address = new Address(dto.Street1, dto.Street2, dto.City, dto.State, dto.PostalCode, dto.Country);
					var orderAdress = new OrderAddress(dto.AddressId, address);
					await StoreAsync(orderAdress);
					return orderAdress;
				}
			}
			return Result.NotFound();
		}

		public Task<Result> StoreAsync(OrderAddress orderAddress)
		{
			return _redisCache.StoreAsync(orderAddress);
		}
	}
}
