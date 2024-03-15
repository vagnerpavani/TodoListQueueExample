using firstproject.Models.DTOs;
using firstproject.Models.Domain;
using firstproject.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase {
    
    private readonly UserService _userService;
    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] CreateUserDto createUserDto)
    {
        User newUser = await _userService.Create(createUserDto);
        return CreatedAtAction(null, null, newUser);
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] UserSignInDto credentials)
    {
        string? token = await _userService.Login(credentials);
        
        return CreatedAtAction(null, null, token);
    }
}