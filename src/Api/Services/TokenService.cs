using Api.Models;
using Infrastructure.Extensions.Security;
using Infrastructure.Security.Azure.KeyVault;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface ITokenService
    {
        Task<string> Generate(UserModel userModel);
    }

    public class TokenService : ITokenService
    {
        private readonly IKeyVaultSecretManager _keyVaultSecretManager;
        private readonly KeyVaultSettings _keyVaultSettings;
        private readonly BearerSecurityKey _bearerSecurityKey;

        public TokenService(IOptionsMonitor<KeyVaultSettings> keyVaultSettings,
            IOptionsMonitor<BearerSecurityKey> bearerSecurityKey,
            IKeyVaultSecretManager keyVaultSecretManager)
        {
            _keyVaultSecretManager = keyVaultSecretManager;
            _keyVaultSettings = keyVaultSettings.CurrentValue;
            _bearerSecurityKey = bearerSecurityKey.CurrentValue;
        }

        public async Task<string> Generate(UserModel userModel)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            // var key = await _keyVaultSecretManager.Get(_bearerSecurityKey.JwtSecurityKey);
            var key = Encoding.ASCII.GetBytes(_bearerSecurityKey.JwtSecurityKey);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userModel.Username),
                    new Claim(ClaimTypes.Role, userModel.Role),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            if (!string.IsNullOrEmpty(userModel.Feature))
                securityTokenDescriptor.Subject.AddClaim(new Claim("Feature", userModel.Feature));
            
            var token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}