using Book.Application.Contracts.Permissions;
using Book.Domain.Entities.Identity;
using Book.Shared.Constants;
using Book.Shared.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
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
        var newRole = new BookRole
        {
            Name = roleName,
        };
        var role = await _roleManager.FindByNameAsync(newRole.Name);
        if (role == null)
        {
            await _roleManager.CreateAsync(newRole);
            role = await _roleManager.FindByNameAsync(newRole.Name);
        }

        var permissions = new List<string>
        {
            BookPermissions.Genres.Default,
            BookPermissions.Genres.Create,
            BookPermissions.Genres.Edit,
            BookPermissions.Genres.Delete,
            BookPermissions.Authors.Default,
            BookPermissions.Authors.Create,
            BookPermissions.Authors.Edit,
            BookPermissions.Authors.Delete,
        };

        var claims = await _roleManager.GetClaimsAsync(role);

        foreach (var permission in permissions.Except(claims.Select(s => s.Value)))
        {
            await _roleManager.AddClaimAsync(role, new Claim("Permission", permission));
        }

        foreach (var claim in claims.ExceptBy(permissions, s => s.Value))
        {
            await _roleManager.RemoveClaimAsync(role, claim);
        }
    }
}
