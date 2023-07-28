using Book.Server.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace Book.Server.Handlers;

public class PoliciesAuthorizationHandler:AuthorizationHandler<CustomUserClaimRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomUserClaimRequirement requirement)
    {
        if (context.User == null || context.User.Identity == null || !context.User.Identity.IsAuthenticated) {
            context.Fail();
            return Task.CompletedTask;
        }

        var hasClaim = context.User.Claims.Any(s => s.Value == requirement.ClaimType);
        if (hasClaim) {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        context.Fail();
        return Task.CompletedTask;
    }
}
