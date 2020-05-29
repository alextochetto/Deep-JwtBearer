namespace Infrastructure.Security.Azure.KeyVault
{
    public class KeyVaultSettings
    {
        public string KeyVaultUrl { get; set; }
        public string AzureTenantId { get; set; }
        public string AzureApplicationClientId { get; set; }
        public string AzureApplicationClientSecret { get; set; }
    }
}