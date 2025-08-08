using Barghto_Ticketing.Models.Entities;

namespace Barghto_Ticketing.Interfaces.Auth;

public interface IJWTTokenService
{
    string CreateJwtToken(User user);
    string RefreshToken(string refreshtoken);
    string GetRefreshTokenHash(string refreshToken);
}
