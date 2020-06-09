using System.Text;
using System.Threading.Tasks;
using Azure.Security.KeyVault.Secrets;

namespace Infrastructure.Security.Azure.KeyVault
{
    public class KeyVaultSecretManager : IKeyVaultSecretManager
    {
        private readonly SecretClient _secretClient;

        public KeyVaultSecretManager(IKeyVaultSecretClientFactory keyVaultSecretClientFactory)
        {
            _secretClient = keyVaultSecretClientFactory.SecretClient;
        }

        public async Task Set(string secretName, string secretValue)
        {
            await _secretClient.SetSecretAsync(secretName, secretValue);
        }

        public async Task<byte[]> Get(string secretName)
        {
            KeyVaultSecret secret = await _secretClient.GetSecretAsync(secretName);
            return secret != null ? Encoding.ASCII.GetBytes(secret.Value) : null;
        }

        public async Task Update(string secretName, string secretValue)
        {
            await Set(secretName, secretValue);
        }

        public async Task Delete(string secretName)
        {
            var operation = await _secretClient.StartDeleteSecretAsync(secretName);
        }
    }
}