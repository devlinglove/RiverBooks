using Ardalis.Result;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RiverBooks.Orders
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
		public async Task<Result> StoreAsync(OrderAddress orderAddress)
		{
			var key = orderAddress.Id.ToString();
			var addressJson = JsonSerializer.Serialize(orderAddress);

			await _db.StringSetAsync(key, addressJson);

			return Result.Success();
		}
	}
}
