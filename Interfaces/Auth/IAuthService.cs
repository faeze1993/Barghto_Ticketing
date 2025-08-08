namespace Barghto_Ticketing.Interfaces.Auth;

public interface IAuthService
{
    Task<Dtos.AuthenticateResDto> Authenticate(string email, string password);
}
