using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using To_Do_List.Api.Models;
using To_Do_List.Core.Domain.Entities;
using To_Do_List.Core.DomainService.Services;

namespace To_Do_List.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenGenerator _tokenGenerator;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenGenerator = tokenGenerator;
    }


    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RequestUserRegister request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new User { UserName = request.username, Email = request.email };
        var result = await _userManager.CreateAsync(user, request.password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        var usercreated = await _userManager.FindByNameAsync(request.username);
        var jwtToken = _tokenGenerator.GenerateJwtTokenAsync(usercreated);

        return Ok(new ResponseLogin { token = jwtToken });
    }


    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] RequestUserLogin request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByEmailAsync(request.email);
        if (user == null)
            return Unauthorized(new { Message = "Invalid email or password" });

        var result = await _signInManager.PasswordSignInAsync(user.UserName, request.password, false, false);
        if (!result.Succeeded)
            return Unauthorized(new { Message = "Invalid email or password" });

        var token = _tokenGenerator.GenerateJwtTokenAsync(user);
        return Ok(new { Token = token });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { Message = "User logged out successfully" });
    }

}

