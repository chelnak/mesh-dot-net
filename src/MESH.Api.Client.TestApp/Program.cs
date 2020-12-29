using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MESH.Api.Client;
using MESH.Api.Client.Configuration;
using MESH.Api.Client.Extensions;

namespace MESH.Api.Client.TestApp
{
    class Program
    {
        public static async Task Main(string[] args)
        {

            var configuration = new MESHApiClientConfiguration
            {
                BaseUrl = "https://localhost:8000",
                CertificateThumbprint = "37af35e3f13c65ea0d7bbad148332924a1ce41d7",
                MailBoxId = "12345",
                MailBoxPassword = "password",
                SharedKey = "shared_key"
            };

            var services = new ServiceCollection()
                .AddMESHApiClient(configuration)
                .BuildServiceProvider();

            var client = services.GetService<IMESHApiClient>();

            var messageCount = await client.GetMessageCount();
            Console.WriteLine($"Count {messageCount.Count}");

            var messages = await client.GetMessages();
            messages.Messages.ForEach(m => Console.WriteLine(m));

        }
    }
}
