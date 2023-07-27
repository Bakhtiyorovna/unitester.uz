using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Unitester_Domain.Entities.Users;
using Unitester_Service.Interfaces.Auth;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Unitester_Service.Comman.Helpers;

namespace Unitester_Service.Services.Auth;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    public TokenService(IConfiguration configuration)
    {
        this._config = configuration.GetSection("Jwt");

    }
    public  string GeneratedToken(User user)
    {
        var identityClaims = new Claim[]
       {
            new Claim("Id", user.Id.ToString()),
            new Claim("FirstName", user.FirstName),
            new Claim("Lastname", user.LastName),
            new Claim("Username", user.UserName),
            new Claim("Email", user.Email),
            new Claim("Region", user.Region.ToString()),
            new Claim("CreateAt",user.CreatedAt.ToString()),
            new Claim("UpdateAt",user.UpdatedAt.ToString()),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            new Claim(ClaimTypes.Role, user.Rol.ToString())
       };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecurityKey"]!));
        var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        int expiresHours = int.Parse(_config["Lifetime"]!);
        var token = new JwtSecurityToken(
            issuer: _config["Issuer"],
            audience: _config["Audience"],
            claims: identityClaims,
            expires: TimeHelper.GetDateTime().AddHours(expiresHours),
            signingCredentials: keyCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
