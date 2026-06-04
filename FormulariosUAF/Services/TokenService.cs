using System.Security.Cryptography;

namespace FormulariosUAF.Services;

public class TokenService : ITokenService
{
    public string GenerateClientToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(bytes)
            .Replace("+", "-")
            .Replace("/", "_")
            .Replace("=", "");
    }

    public bool IsTokenValid(string token, DateTime expiry)
    {
        return !string.IsNullOrEmpty(token) && expiry > DateTime.UtcNow;
    }
}
