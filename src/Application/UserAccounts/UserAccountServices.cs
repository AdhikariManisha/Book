using Book.Application.Contracts.Repositories;
using Book.Application.Contracts.UserAccounts;
using Book.Domain.Entities;
using Book.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Application.UserAccounts;
public class UserAccountServices : IUserAccountServices
{
    private readonly IRepository<IdentityUser, int> _repository;

    public UserAccountServices(IRepository<IdentityUser, int> repository)
    {
        _repository = repository;
    }

    public Task<bool> ChangePasswordAsync(UserChangePasswordDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ForgetPasswordAsync(UserForgetPasswordDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> LoginAsync(UserLoginDto dto)
    {
        var user = await _repository.Entities.Where(s => s.UserName == dto.UserName).FirstOrDefaultAsync();
        if (user == null) {
            throw new Exception("Invalid Username/Password.");
        }

        // password
        byte[] passwordBytes = System.Text.Encoding.ASCII.GetBytes(dto.Password);
        passwordBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordBytes);
        String passwordHash = System.Text.Encoding.ASCII.GetString(passwordBytes);

        if (user.PasswordHash != passwordHash)
        {
            throw new Exception("Invalid Username/Password.");
        }
        return true;
    }

    public Task<bool> RegisterAsync(UserRegisterDto dto)
    {
        throw new NotImplementedException();
    }
}
