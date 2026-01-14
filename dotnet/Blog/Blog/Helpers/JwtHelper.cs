using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blog.Models;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Helpers;

public class JwtHelper
{
    private const string DefaultSecretKey = "this-is-a-very-secure-secret-key-32";

    public static Task<string> GenerateToken(
        User user,
        TimeSpan expiration,
        string secretKey = DefaultSecretKey
    )
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Login),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: DateTime.UtcNow.Add(expiration),
            signingCredentials: creds
        );

        return Task.FromResult(
            new JwtSecurityTokenHandler().WriteToken(token)
        );
    }

    public static ClaimsPrincipal? ValidateToken(
        string token,
        string secretKey = DefaultSecretKey
    )
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(secretKey);

        try
        {
            var principal = tokenHandler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // без “поблажки” в 5 минут
                },
                out SecurityToken validatedToken
            );

            return principal;
        }
        catch
        {
            return null;
        }
    }

    public static Task<int?>? GetUserIdFromToken(
        string token,
        string secretKey = DefaultSecretKey
    )
    {
        var principal = ValidateToken(token, secretKey);
        if (principal == null)
            return null;

        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
        var data = userIdClaim != null ? int.Parse(userIdClaim.Value) : (int?)null;
        
        return Task.FromResult(data);
    }
}
