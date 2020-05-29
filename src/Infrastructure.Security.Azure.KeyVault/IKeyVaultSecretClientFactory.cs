using Azure.Security.KeyVault.Secrets;

namespace Infrastructure.Security.Azure.KeyVault
{
    public interface IKeyVaultSecretClientFactory
    {
        public SecretClient SecretClient { get; }
    }
}