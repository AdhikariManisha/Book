using Book.Domain.Entities.Identity;
using Book.Shared.Constants;
using Book.Shared.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Book.Infrastructure.Seeders;
public class RoleDataSeeder : IDataSeeder
{
    private readonly RoleManager<BookRole> _roleManager;

    public RoleDataSeeder(RoleManager<BookRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<bool> SeedAsync()
    {
        var roles = new List<string>{
            Roles.Admin.ToString(),
            Roles.Librarian.ToString(),
            Roles.User.ToString(),
        };

        foreach (var role in roles) {
            await AddRole(role);
        }
        return true;
    }

    private async Task AddRole(string roleName)
    {
        var role = new BookRole { 
            Name = roleName,
        };
        var roleExists = await _roleManager.FindByNameAsync(role.Name);
        if (roleExists == null) {
            await _roleManager.CreateAsync(role);
        }
    }
}
