using Ardalis.Result;
using Microsoft.Extensions.Logging;
using RiverBooks.Orders.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RiverBooks.Orders.Infrastructure
{
	internal class RedisOrderAddressCache : IOrderAddressCache
	{
		private readonly ILogger<RedisOrderAddressCache> _logger;
		private readonly IDatabase _db;

		public RedisOrderAddressCache(
			ILogger<RedisOrderAddressCache> logger

		)
		{
			var redis = ConnectionMultiplexer.Connect("localhost");
			_db = redis.GetDatabase();
			_logger = logger;
		}

		public async Task<Result<OrderAddress>> GetByIdAsync(Guid addressId)
		{
			string? addressJson = await _db.StringGetAsync(addressId.ToString());
			if (addressJson == null) return Result.NotFound();

			var orderAdd = JsonSerializer.Deserialize<OrderAddress>(addressJson);
			if (orderAdd == null) return Result.NotFound();

			return Result.Success(orderAdd);
		}

		public async Task<Result> StoreAsync(OrderAddress orderAddress)
		{
			var key = orderAddress.Id.ToString();
			var addressJson = JsonSerializer.Serialize(orderAddress);

			await _db.StringSetAsync(key, addressJson);

			return Result.Success();
		}
	}
}
