using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiAuthSaude.Models;
using Microsoft.IdentityModel.Tokens;

namespace ApiAuthSaude.Services
{
    public class TokenService
    {
        public static string GenerateToken (User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username), // Vai mapear diretamente para User.Identity.Name
                    new Claim(ClaimTypes.Role, user.Role) // Vai mapear para User.IsInRole
                }),

                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials( new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor); //criar token
            return tokenHandler.WriteToken(token);
        }
    }
}