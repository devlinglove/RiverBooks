using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
			logger.Information("{Module} module services registered", "EmailSending");
			return services;
		}
	}
}
