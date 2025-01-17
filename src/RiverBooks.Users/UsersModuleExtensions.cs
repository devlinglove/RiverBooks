using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace RiverBooks.Users;

public static class UsersModuleExtensions
{

  public static IServiceCollection AddUsersModuleServices(
    this IServiceCollection services, 
    IConfiguration config,
    ILogger logger
    )
  {

    services.AddDbContext<UserDbContext>(options => options.UseSqlServer(config.GetConnectionString("UsersConnectionString")));
	
	services.AddTransient<ITokenService, TokenService>();

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


		services.AddAuthentication(opt =>
		{
			opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])),
				ValidIssuer = config["Jwt:Issuer"],
				ValidateIssuer = true,
				
				//Audience (resource server - endpoint with token)
				//ValidateAudience = false,
				//ValidateLifetime = true,
				//ClockSkew = TimeSpan.Zero
			};
		});



	logger.Information("{Module} module services registered", "Users");
    return services;
  }

}
