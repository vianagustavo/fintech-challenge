using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FintechChallenge.Models;
using Microsoft.IdentityModel.Tokens;

namespace FintechChallenge.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(People people)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var secretKey = _configuration["Authentication:SecretKey"];
        Console.WriteLine(secretKey);
        var key = Encoding.ASCII.GetBytes(secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, people.Id.ToString()),
                new Claim(ClaimTypes.Role, "people")
            }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}