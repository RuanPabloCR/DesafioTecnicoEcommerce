using DesafioTecnicoEcommerce.ApiGateway.Application.Interfaces;
using DesafioTecnicoEcommerce.ApiGateway.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DesafioTecnicoEcommerce.ApiGateway.Infrastructure.JWT
{
    public class TokenService : ITokenService
    {
        private readonly string _secretKey;
        public TokenService(string secretKey)
        {
            _secretKey = secretKey;
        }
        public string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey));
            var credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = CreateIdentity(user),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = credential,
            };
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        private static ClaimsIdentity CreateIdentity(User user)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            ci.AddClaim(new Claim(ClaimTypes.Name, user.Name.Name));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.Email.EmailAddress));
            foreach (var role in user.Roles)
            {
                ci.AddClaim(new Claim(ClaimTypes.Role, role));
            }
            return ci;
        }
    }
}
