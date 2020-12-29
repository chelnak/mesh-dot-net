using MESH.Api.Client.Authentication;
using MESH.Api.Client.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

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
                    handler.ClientCertificates.Add(GetCertificate(configuration.CertificateThumbprint));
                    handler.ServerCertificateCustomValidationCallback = (m, c, ch, e) => true;

                    return handler;
                });

            return services;
        }

        private static X509Certificate2 GetCertificate(string thumbprint)
        {
            using (X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                certStore.Open(OpenFlags.ReadOnly);

                X509Certificate2Collection certCollection = certStore.Certificates.Find(
                                            X509FindType.FindByThumbprint,
                                            thumbprint.ToUpper(),
                                            false);

                // Get the first cert with the thumbprint
                X509Certificate2 cert = (X509Certificate2)certCollection.OfType<X509Certificate>().FirstOrDefault();

                if (cert is null)
                    throw new Exception($"Certificate with thumbprint {thumbprint} was not found");

                return cert;
            }
        }
    }
}
