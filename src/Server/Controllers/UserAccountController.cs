using Book.Application.Contracts.Repositories;
using Book.Application.Contracts.UserAccounts;
using Book.Domain.Entities;
using Book.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book.Server.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class UserAccountController : ControllerBase
{
    private readonly IUserAccountServices _userAccount;

    public UserAccountController(IUserAccountServices userAccount) {
        _userAccount = userAccount;
    }
    [Authorize("Book.User.Login")]
    [HttpPost("login")]
    public async Task<ActionResult> LoginAsync(UserLoginDto dto)
    {
        var isSuccess = await _userAccount.LoginAsync(dto);
        return Ok(isSuccess);
    }
}

