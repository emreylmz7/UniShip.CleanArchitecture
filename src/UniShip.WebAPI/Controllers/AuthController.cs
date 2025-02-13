using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniShip.Application.Features.Auth.Login;

namespace UniShip.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LoginCommand model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new IdentityUser { Email = model.EmailOrUserName };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _signInManager.CheckPasswordSignInAsync(model.EmailOrUserName, model.Password, false, false);

        if (!result.Succeeded)
            return Unauthorized("Invalid credentials");

        return Ok("Login successful");
    }
}
