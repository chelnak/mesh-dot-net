using MESH.Api.Client.Configuration;
using Moq;
using Moq.Protected;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MESH.Api.Client
{
    public static class MessageHandler
    {
        public static Mock<HttpMessageHandler> SetupMessageHandlerMock(HttpResponseMessage response, MESHApiClientConfiguration config, string endpoint)
        {
            var handshakeUri = $"{config.BaseUrl}/messageexchange/{config.MailBoxId}";

            if (!endpoint.StartsWith("/"))
            {
                endpoint = $"{endpoint}/";
            }

            var expectedUri = $"{config.BaseUrl}/{endpoint}";

            var httpMessageHandler = new Mock<HttpMessageHandler>();
            httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(c =>
                        c.Method.Equals(HttpMethod.Get)
                        && ( c.RequestUri.AbsoluteUri.Equals(handshakeUri) || c.RequestUri.AbsoluteUri.Equals(expectedUri) )
                        ),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync((HttpRequestMessage request, CancellationToken token) => response);
            return httpMessageHandler;
        }
    }
}
