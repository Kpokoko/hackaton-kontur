using Domain;

namespace MockServiceApplication.Identity.JWT;

public interface IJwtProvider
{
    string GenerateToken(User user);
}