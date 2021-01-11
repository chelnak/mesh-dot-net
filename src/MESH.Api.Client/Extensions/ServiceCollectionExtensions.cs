using MESH.Api.Client.Configuration;
using MESH.Api.Client.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace MESH.Api.Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMESHApiClient(this IServiceCollection services, MESHApiClientConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddTransient<MESHHttpClientHandler>();
            services.AddHttpClient<IMESHApiClient, MESHApiClient>()
                .ConfigurePrimaryHttpMessageHandler<MESHHttpClientHandler>();

            return services;
        }

    }
}
