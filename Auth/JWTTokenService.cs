using Barghto_Ticketing.Interfaces.Auth;
using Barghto_Ticketing.Models.Entities;
using Barghto_Ticketing.Models.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Barghto_Ticketing.Auth;

public class JWTTokenService : IJWTTokenService
{
    private readonly IConfiguration _configuration;
    public JWTTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string CreateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.ToString())
        };
        var key = _configuration["JWtConfig:Key"];
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokenexp = DateTime.Now.AddMinutes(int.Parse(_configuration["JWtConfig:expires"]));
        var token = new JwtSecurityToken(
            issuer: _configuration["JWtConfig:issuer"],
            audience: _configuration["JWtConfig:audience"],
            expires: tokenexp,
            notBefore: DateTime.Now,
            claims: claims,
            signingCredentials: credentials 
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GetRefreshTokenHash(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public string RefreshToken(string Refreshtoken)
    {
        throw new NotImplementedException();
    }
}
