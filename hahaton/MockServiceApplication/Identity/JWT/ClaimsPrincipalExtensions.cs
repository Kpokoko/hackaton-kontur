using System.Security.Claims;

namespace MockServiceApplication.Identity.JWT;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst("UserId")?.Value;
        
        if (string.IsNullOrWhiteSpace(userIdClaim))
        {
            throw new Exception();
        }

        return Guid.TryParse(userIdClaim, out var userId) 
            ? userId 
            : throw new Exception();
    }
}