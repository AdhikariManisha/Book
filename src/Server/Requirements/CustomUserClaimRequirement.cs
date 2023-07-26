using Microsoft.AspNetCore.Authorization;

namespace Book.Server.Requirements;

public class CustomUserClaimRequirement : IAuthorizationRequirement
{
    public CustomUserClaimRequirement(string claimType)
    {
        ClaimType = claimType;
    }

    public string ClaimType { get; set; }
}
