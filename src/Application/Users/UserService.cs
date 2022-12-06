using AutoMapper;
using Book.Application.Contracts.Repositories;
using Book.Application.Contracts.Users;
using Book.Domain.Entities.Identity;
using Book.Shared.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Book.Application.Users;
public class UserService : IUserService
{
    private readonly UserManager<BookUser> _userManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<BookUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public Task<bool> ChangePasswordAsync(UserChangePasswordDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ForgetPasswordAsync(UserForgetPasswordDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<List<UserDto>> GetListAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var dtoUsers = _mapper.Map<List<UserDto>>(users);

        return dtoUsers;
    }

    public async Task<bool> LoginAsync(UserLoginDto dto)
    {
        var userExists = await _userManager.FindByNameAsync(dto.UserName);
        if (userExists == null)
        {
            throw new Exception("Invalid Username/Password");
        }

        bool isValidPassword = await _userManager.CheckPasswordAsync(userExists, dto.Password);

        if (!isValidPassword)
        {
            throw new Exception("Invalid Username/Password.");
        }
        return true;
    }

    public async Task<bool> RegisterAsync(UserRegisterDto dto)
    {
        var user = _mapper.Map<BookUser>(dto);

        var userExists = await _userManager.FindByNameAsync(dto.UserName);
        if (userExists != null) {
            throw new Exception("User already exists");
        }

        await _userManager.CreateAsync(user, dto.Password);
        await _userManager.AddToRoleAsync(user, Roles.User.ToString());

        return true;
    }
}
