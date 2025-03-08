using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Repositories;
using Serilog;
using System.Reflection;
using System.Text;
using static RiverBooks.Users.UserDbContext;

namespace RiverBooks.Users;

public static class UsersModuleExtensions
{
	public static IServiceCollection AddUsersModuleServices(
    this IServiceCollection services, 
    IConfiguration config,
    ILogger logger,
	List<Assembly> mediateRAssemblies
    )
	{
    services.AddDbContext<UserDbContext>(options => options.UseSqlServer(config.GetConnectionString("UsersConnectionString")));
	services.AddTransient<ITokenService, TokenService>();
	services.AddTransient<IApplicationUserRepository, EfApplicationUserRepository>();
	services.AddIdentityCore<ApplicationUser>(options =>
	{
		// password configuration
		//options.Password.RequiredLength = 6;
		//options.Password.RequireDigit = false;
		//options.Password.RequireLowercase = false;
		//options.Password.RequireUppercase = false;
		//options.Password.RequireNonAlphanumeric = false;

		// for email confirmation
		//options.SignIn.RequireConfirmedEmail = true;
		//options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultEmailProvider;
	})
	.AddRoles<IdentityRole>()
	.AddRoleManager<RoleManager<IdentityRole>>()
	.AddEntityFrameworkStores<UserDbContext>()
	.AddSignInManager<SignInManager<ApplicationUser>>()
	.AddUserManager<UserManager<ApplicationUser>>()
	.AddDefaultTokenProviders();

	services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
	services.AddScoped<UserContextSeedService>();
	mediateRAssemblies.Add(typeof(UsersModuleExtensions).Assembly);
	logger.Information("{Module} module services registered", "Users");
	return services;
  }

	public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider, ILogger logger)
	{
		using var scope = serviceProvider.CreateScope();
		try
		{
			var contextSeedService = scope.ServiceProvider.GetService<UserContextSeedService>();
			await contextSeedService.InitializeContextAsync();
		}
		catch (Exception ex)
		{
			//var logger = scope.ServiceProvider.GetService<Program>();
			logger.Information(ex.Message, "Failed to initialize and seed the database");
		}

	}

}
