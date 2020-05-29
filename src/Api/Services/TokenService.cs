using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Api.Configuration;
using Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using System.Linq;

namespace Api.Services
{
    public static class TokenService
    {
        public static string Generate(UserModel userModel)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userModel.Username),
                    new Claim(ClaimTypes.Role, userModel.Role),
                    new Claim("Feature", userModel.Feature),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}