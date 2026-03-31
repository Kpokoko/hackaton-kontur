namespace MockServiceApplication.Identity.JWT;

public class JwtOptions
{
    public string SecretKey { get; init; } = String.Empty;
    public int ExpiresHours { get; init; }
}