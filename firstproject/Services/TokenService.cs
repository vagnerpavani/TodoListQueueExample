using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using firstproject.Config;

namespace firstproject.Services;

public class TokenService(IOptions<TokenConfig> tokenConfig)
{
  private readonly string _key = tokenConfig.Value.Key;
  private readonly string _issuer = tokenConfig.Value.Issuer;
  private readonly string _audience = tokenConfig.Value.Audience;

  public string GenerateToken(long userId)
  {
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: _issuer,
        audience: _audience,
        claims:
        [
          new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        ],
        expires: DateTime.UtcNow.AddHours(24),
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}