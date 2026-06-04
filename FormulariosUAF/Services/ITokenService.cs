namespace FormulariosUAF.Services;

public interface ITokenService
{
    string GenerateClientToken();
    bool IsTokenValid(string token, DateTime expiry);
}
