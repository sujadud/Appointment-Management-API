using Appointment_Management.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Appointment_Management.Infrastructure.Services;

public class TokenGenerator
{
    private readonly IConfiguration _config;

    public TokenGenerator(IConfiguration config)
    {
        _config = config;
    }

    public async Task<string> GenerateJwtToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_config["JwtSettings:Secret"]);

        var securityKey = new SymmetricSecurityKey(key)
        {
            KeyId = "E7413E868C28A32FBFB70FA3992ED0A5D5B64A73"
        };

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddHours(2),
            Issuer = _config["JwtSettings:Issuer"],
            Audience = _config["JwtSettings:Audience"],
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

        if (!jwtToken.Header.ContainsKey("kid"))
        {
            jwtToken.Header.Add("kid", securityKey.KeyId);
        }

        return await Task.FromResult(tokenHandler.WriteToken(jwtToken));
    }
}
