using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BasketBuddy.Api;

public struct TokenHelpers
{
    static public string GenerateToken()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersecretkeysupersecretkeysupersecretkey"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] 
        {
            new Claim("anon", Guid.NewGuid().ToString()) 
        };

        var token = new JwtSecurityToken("basketbuddy-api",
            "anon",
            claims,
            expires: DateTime.Now.AddYears(100),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}