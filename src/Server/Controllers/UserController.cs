using Book.Application.Contracts.Repositories;
using Book.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Book.Server.Controllers;

public class UserController : ApplicationService<User, int>
{
    public UserController(IRepository<User, int> repository) : base(repository)
    {
    }

    public override async Task<ActionResult> UpdateAsync(int id, User input)
    {
        // validating user
        var user = await Repository.GetAsync(id);
        if (user == null) {
            throw new Exception("User doesnot exists.");
        }

        user.UserName = input.UserName;  
        user.Name = input.Name;  
        user.Surname = input.Surname;  
        user.DOB = input.DOB;
        user.Address = input.Address;

        // password
        byte[] passwordBytes = System.Text.Encoding.ASCII.GetBytes(input.Password);
        passwordBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordBytes);
        String passwordHash = System.Text.Encoding.ASCII.GetString(passwordBytes);
        user.Password = passwordHash;

        return await base.UpdateAsync(id, user);
    }
}
