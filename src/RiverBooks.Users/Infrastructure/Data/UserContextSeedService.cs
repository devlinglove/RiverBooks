using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RiverBooks.Users.Domain;
using System.Security.Claims;


namespace RiverBooks.Users.Infrastructure.Data
{
	public class UserContextSeedService
	{
		private readonly UserDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserContextSeedService(UserDbContext context,
			UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task InitializeContextAsync()
		{
			if (_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Count() > 0)
			{
				// applies any pending migration into our database
				await _context.Database.MigrateAsync();
			}

			if (!_roleManager.Roles.Any())
			{
				await _roleManager.CreateAsync(new IdentityRole { Name = SD.AdminRole });
				await _roleManager.CreateAsync(new IdentityRole { Name = SD.ManagerRole });
				await _roleManager.CreateAsync(new IdentityRole { Name = SD.PlayerRole });
				await _roleManager.CreateAsync(new IdentityRole { Name = SD.UserRole });
			}

			if (!_userManager.Users.AnyAsync().GetAwaiter().GetResult())
			{
				var admin = new ApplicationUser
				{
					//FirstName = "admin",
					//LastName = "jackson",
					UserName = SD.AdminUserName,
					Email = SD.AdminUserName,
					EmailConfirmed = false
				};
				await _userManager.CreateAsync(admin, "Zxcvb123@");

				await _userManager.AddToRolesAsync(admin, new[] { SD.AdminRole, SD.ManagerRole, SD.PlayerRole });
				await _userManager.AddClaimsAsync(admin, new Claim[]
				{
					new Claim(ClaimTypes.Email, admin.Email),
					//new Claim(ClaimTypes.Surname, admin.LastName)
				});

				var manager = new ApplicationUser
				{
					//FirstName = "manager",
					//LastName = "wilson",
					UserName = "manager@example.com",
					Email = "manager@example.com",
					EmailConfirmed = false
				};
				await _userManager.CreateAsync(manager, "Zxcvb123@");
				await _userManager.AddToRoleAsync(manager, SD.ManagerRole);
				await _userManager.AddClaimsAsync(manager, new Claim[]
				{
					new Claim(ClaimTypes.Email, manager.Email),
					//new Claim(ClaimTypes.Surname, manager.LastName)
				});

				var player = new ApplicationUser
				{
					//FirstName = "player",
					//LastName = "miller",
					UserName = "player@example.com",
					Email = "player@example.com",
					EmailConfirmed = true
				};
				await _userManager.CreateAsync(player, "Zxcvb123@");
				await _userManager.AddToRoleAsync(player, SD.PlayerRole);
				await _userManager.AddClaimsAsync(player, new Claim[]
				{
					new Claim(ClaimTypes.Email, player.Email),
					//new Claim(ClaimTypes.Surname, player.LastName)
				});

				var vipplayer = new ApplicationUser
				{
					//FirstName = "vipplayer",
					//LastName = "tomson",
					UserName = "vipplayer@example.com",
					Email = "vipplayer@example.com",
					EmailConfirmed = false
				};
				await _userManager.CreateAsync(vipplayer, "Zxcvb123@");
				await _userManager.AddToRoleAsync(vipplayer, SD.PlayerRole);
				await _userManager.AddClaimsAsync(vipplayer, new Claim[]
				{
					new Claim(ClaimTypes.Email, vipplayer.Email),
					//new Claim(ClaimTypes.Surname, vipplayer.LastName)
				});
			}
		}



	}
}
