using Infrastructure.Security.Azure.KeyVault;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);

                    config.AddEnvironmentVariables();
                    
                    var configuration = config.Build();

                    config.AddAzureKeyVault(configuration[$"{nameof(KeyVaultSettings)}:{nameof(KeyVaultSettings.KeyVaultUrl)}"], 
                        configuration[$"{nameof(KeyVaultSettings)}:{nameof(KeyVaultSettings.AzureApplicationClientId)}"],  
                        configuration[$"{nameof(KeyVaultSettings)}:{nameof(KeyVaultSettings.AzureApplicationClientSecret)}"],
                        new DefaultKeyVaultSecretManager());
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}