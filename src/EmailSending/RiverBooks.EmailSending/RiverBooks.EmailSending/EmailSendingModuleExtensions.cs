using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Serilog;
using System.Reflection;



namespace RiverBooks.EmailSending
{
	public static class EmailSendingModuleExtensions
	{
		public static IServiceCollection AddEmailSendingServices(
			this IServiceCollection services,
			IConfiguration config,
			ILogger logger,
			List<Assembly> mediateRAssemblies
		)
		{
			services.AddTransient<ISenderEmail, MimeKitEmailSender>();
			mediateRAssemblies.Add(typeof(EmailSendingModuleExtensions).Assembly);

			BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

			services.Configure<MongoDBSettings>(config.GetSection("MongoDB"));
			services.AddMongoDB(config);

			services.AddScoped<IOutboxService, MongoDbService>();

			logger.Information("{Module} module services registered", "EmailSending");
			return services;
		}

		public static IServiceCollection AddMongoDB(this IServiceCollection services,
	IConfiguration configuration)
		{
			// Register the MongoDB client as a singleton
			services.AddSingleton<IMongoClient>(serviceProvider =>
			{
				var settings = configuration.GetSection("MongoDB").Get<MongoDBSettings>();
				return new MongoClient(settings!.ConnectionString);
			});

			// Register the MongoDB database as a singleton
			services.AddSingleton(serviceProvider =>
			{
				var settings = configuration.GetSection("MongoDB").Get<MongoDBSettings>();
				var client = serviceProvider.GetService<IMongoClient>();
				return client!.GetDatabase(settings!.DatabaseName);
			});

			//// Optionally, register specific collections here as scoped or singleton services
			//// Example for a 'EmailOutboxEntity' collection
			services.AddTransient(serviceProvider =>
			{
				var database = serviceProvider.GetService<IMongoDatabase>();
				return database!.GetCollection<EmailOutboxEntity>("EmailOutboxEntityCollection");
			});

			return services;
		}




	}
}
