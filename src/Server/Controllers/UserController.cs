using Book.Application.Contracts.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Book.Server.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService) {
        _userService = userService;
    }
    //[Authorize("Book.User.Login")]
    [HttpPost("login")]
    public async Task<ActionResult> LoginAsync(UserLoginDto dto)
    {
        var isSuccess = await _userService.LoginAsync(dto);
        return Ok(isSuccess);
    }
    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync(UserRegisterDto dto)
    {
        var isSuccess = await _userService.RegisterAsync(dto);
        return Ok();
    }
    [HttpGet]
    public async Task<ActionResult> GetListAsync() {
        var users = await _userService.GetListAsync();
        return Ok(users);
    }
}


