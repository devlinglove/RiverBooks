

using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiverBooks.Users.Domain;
using RiverBooks.Users.DTOs;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{

  private readonly UserManager<ApplicationUser> _userManager;
  private readonly SignInManager<ApplicationUser> _signInManager;
  private readonly ITokenService _tokenService;
  //private readonly ISender _sender;

  public AccountController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    ITokenService tokenService
    ///ISender sender
    )
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _tokenService = tokenService;
    //_sender = sender;
  }

  [HttpPost("register")]
  public async Task<IActionResult> AddUser (RegisterDto model)
  {
    if (await CheckEmailExistsAsync(model.Email))
    {
      return BadRequest($"An existing account is using {model.Email}, email addres. Please try with another email address");
    }

    var userToAdd = new ApplicationUser
    {
      UserName = model.Email.ToLower(),
      Email = model.Email.ToLower(),
    };

    var result = await _userManager.CreateAsync(userToAdd, model.Password);
    if (!result.Succeeded) return BadRequest(result.Errors);

    await _userManager.AddToRoleAsync(userToAdd, "User");
    return Ok(new RegisterResponse(model.Email));

  }

	[HttpPost("login")]
	public async Task<ActionResult<UserDto>> Login(LoginDto model)
	{
		var user = await _userManager.FindByNameAsync(model.Email);
		if (user == null || user.Email == null) return Unauthorized("Invalid username or password");

        

		var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

		if (result.Succeeded)
		{
			var roles = await _userManager.GetRolesAsync(user);

			return Ok(new UserDto
			{
				Email = user.Email,
				Roles = roles,
				Token = _tokenService.CreateToken(user, roles)
			});

		}

		return Unauthorized("Invalid username or password");

	}


	private async Task<bool> CheckEmailExistsAsync(string email)
    {
        return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());
    }

	

}
