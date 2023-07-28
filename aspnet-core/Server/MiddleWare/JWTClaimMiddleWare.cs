using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Book.Server.MiddleWare;

public class JwtClaimsMiddleWare
{
    private readonly RequestDelegate _next;

    public JwtClaimsMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var authResult = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
        if (authResult?.Principal != null) {
            context.User = authResult.Principal;
        }

        await _next(context);
    }
}
