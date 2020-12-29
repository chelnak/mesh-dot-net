using MESH.Api.Client.Authentication;
using MESH.Api.Client.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace MESH.Api.Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMESHApiClient(this IServiceCollection services, MESHApiClientConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddSingleton<IAuthenticationHeaderGenerator, AuthenticationHeaderGenerator>();
            services.AddHttpClient<IMESHApiClient, MESHApiClient>()
                .ConfigureHttpClient(client =>
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Mex-ClientVersion", "ApiDocs == 0.0.1");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Mex-OSArchitecture", "x86_64");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Mex-OSName", "Windows");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Mex-OSVersion", "#44~18.04.2-Ubuntu");
                })
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    var handler = new HttpClientHandler();

                    handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    handler.ClientCertificates.Add(CertificateHelper.GetCertificate(configuration.CertificateThumbprint));
                    handler.ServerCertificateCustomValidationCallback = (m, c, ch, e) => true;

                    return handler;

                });

            return services;

        }
    }
}
