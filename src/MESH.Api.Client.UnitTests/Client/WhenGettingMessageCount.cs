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
    public class WhenGettingMessageCount
    {
        [Test, MESHAutoData]
        public async Task The_Correct_Count_Is_Returned(
            MESHApiClientConfiguration config,
            GetMessageCountResponse getMessageCountResponse
        )
        {
            var expectedEndpoint = $"/messageexchange/{config.MailBoxId}/count";

            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(getMessageCountResponse)),
                StatusCode = HttpStatusCode.OK
            };

            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, config, expectedEndpoint);
            var httpClient = new HttpClient(httpMessageHandler.Object);

            var client = new MESHApiClient(httpClient, config);

            var result = await client.GetMessageCount();
            result.Should().BeEquivalentTo(getMessageCountResponse);
        }
    }
}
