﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.Orders.Infrastructure;
using RiverBooks.Orders.Infrastructure.Data;
using RiverBooks.Orders.Interfaces;
using Serilog;
using StackExchange.Redis;
using System.Reflection;

namespace RiverBooks.Orders;

public static class OrdersModuleServiceExtensions
{
	public static IServiceCollection AddOrdersModuleServices(
    this IServiceCollection services, 
    IConfiguration config,
    ILogger logger,
	List<Assembly> mediateRAssemblies
    )
	{
		services.AddDbContext<OrderDbContext>(options => options.UseSqlServer(config.GetConnectionString("OrdersConnectionString")));
		services.AddTransient<IOrderRepository, EfOrderRepository>();
		//services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));
		//services.AddHttpClient();
		services.AddScoped<RedisOrderAddressCache>();
		services.AddScoped<IOrderAddressCache, ReadThroughOrderAddressCache>();


		mediateRAssemblies.Add(typeof(OrdersModuleServiceExtensions).Assembly);
		logger.Information("{Module} module services registered", "Orders");
		return services;
	}
}
