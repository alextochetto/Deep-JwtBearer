using System;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Infrastructure.Security.Azure.KeyVault;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Extensions.Security
{
    public static class AzureSecurityKeyVaultExtension
    {
        public static IServiceCollection AddSecurityAzureKeyVault(this IServiceCollection services, IConfiguration configuration)
        {
            var keyVaultSettings = services.BuildServiceProvider()
                .GetService<IOptionsMonitor<KeyVaultSettings>>().CurrentValue;
            
            var keyVaultSecretClientFactory = InitializeSecretClientInstance(configuration, keyVaultSettings);
            services.AddSingleton<IKeyVaultSecretClientFactory>(keyVaultSecretClientFactory);
            services.AddSingleton<IKeyVaultSecretManager, KeyVaultSecretManager>();

            return services;
        }

        private static IKeyVaultSecretClientFactory InitializeSecretClientInstance(IConfiguration configuration, KeyVaultSettings keyVaultSettings)
        {
            TokenCredential credential = new DefaultAzureCredential();
            
#if DEBUG
            credential = new ClientSecretCredential(keyVaultSettings.AzureTenantId, keyVaultSettings.AzureApplicationClientId, keyVaultSettings.AzureApplicationClientSecret);
#endif

            var secretClient = new SecretClient(new Uri(keyVaultSettings.KeyVaultUrl), credential);
            return new KeyVaultSecretClientFactory(secretClient);
        }
    }
}