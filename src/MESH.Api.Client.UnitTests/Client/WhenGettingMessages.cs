using FluentAssertions;
using MESH.Api.Client.Configuration;
using MESH.Api.Client.Entities;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MESH.Api.Client.UnitTests.Client
{
    [TestFixture]
    public class WhenGettingAMessage
    {
        [Test, MESHAutoData]
        public async Task It_Returns_A_List_Of_MessageIds(
            MESHApiClientConfiguration config,
            GetMessagesResponse getMessagesResponse
        )
        {
            var expectedEndpoint = $"/messageexchange/{config.MailBoxId}/inbox";

            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(getMessagesResponse)),
                StatusCode = HttpStatusCode.OK
            };

            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, config, expectedEndpoint);
            var httpClient = new HttpClient(httpMessageHandler.Object);

            var client = new MESHApiClient(httpClient, config);

            var result = await client.GetMessages();
            result.Should().BeEquivalentTo(getMessagesResponse);
        }
    }
}
