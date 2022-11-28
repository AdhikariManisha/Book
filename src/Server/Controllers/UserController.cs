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

        return await base.UpdateAsync(id, user);
    }
}
