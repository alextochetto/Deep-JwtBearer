using System.Threading.Tasks;
using Azure.Security.KeyVault.Secrets;

namespace Infrastructure.Security.Azure.KeyVault
{
    public interface IKeyVaultSecretManager
    {
        Task Set(string secretName, string secretValue);
        Task<string> Get(string secretName);
        Task Delete(string secretName);
        Task Update(string secretName, string secretValue);
    }
}