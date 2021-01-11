using FluentAssertions;
using MESH.Api.Client.Configuration;
using MESH.Api.Client.Entities;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace MESH.Api.Client.UnitTests.Client
{
    [TestFixture]
    public class WhenAcknowledgingAMessage
    {
        [Test, MESHAutoData]
        public async Task It_Returns_AcknowledgeMessageResponse(
            MESHApiClientConfiguration config,
            string messageId
        )
        {
            var expectedEndpoint = $"/messageexchange/{config.MailBoxId}/inbox/{messageId}/status/acknowledged";

            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonSerializer.Serialize(new AcknowledgeMessageResponse { Id = messageId })),
                StatusCode = HttpStatusCode.OK
            };

            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, config, expectedEndpoint);
            var httpClient = new HttpClient(httpMessageHandler.Object);

            var client = new MESHApiClient(httpClient, config);

            var result = await client.AcknowledgeMessage(messageId);
            result.Should().BeEquivalentTo(new AcknowledgeMessageResponse { Id = messageId });
        }
    }
}
