using Barghto_Ticketing.Interfaces.Auth;
using Barghto_Ticketing.Utilities;
using Barghto_Ticketing.Dtos;
using Barghto_Ticketing.Data;
using Microsoft.EntityFrameworkCore;

namespace Barghto_Ticketing.Auth;

public class AuthService : IAuthService
{
    private readonly SqlLiteDBContext _dbContext;
    private readonly IJWTTokenService _jwtTokenService;
    public AuthService(IJWTTokenService jwtTokenService, SqlLiteDBContext dbContext)
    {
        _jwtTokenService = jwtTokenService;
        _dbContext = dbContext;
    }
    public async Task<AuthenticateResDto> Authenticate(string email, string password)
    {
        var user = await _dbContext.User.SingleOrDefaultAsync(u => u.Email == email);

        if (user == null || user.Password != password.Trim().ToHashPass())
        {
            throw new Exception("نام کاربری یا رمز عبور اشتباه می باشد");
        }

        return new AuthenticateResDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Token = _jwtTokenService.CreateJwtToken(user)
        };
    }
}
