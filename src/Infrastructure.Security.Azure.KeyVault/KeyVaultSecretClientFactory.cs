using Azure.Security.KeyVault.Secrets;

namespace Infrastructure.Security.Azure.KeyVault
{
    public class KeyVaultSecretClientFactory : IKeyVaultSecretClientFactory
    {
        public SecretClient SecretClient { get; }

        public KeyVaultSecretClientFactory(SecretClient secretClient)
        {
            SecretClient = secretClient;
        }
    }
}