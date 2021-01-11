using MESH.Api.Client.Authentication;
using MESH.Api.Client.Configuration;
using MESH.Api.Client.Entities;
using System.Text.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MESH.Api.Client
{
    public class MESHApiClient : IMESHApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly MESHApiClientConfiguration _config;
        private readonly IAuthenticationHeaderGenerator _authHeaderGenerator;

        public MESHApiClient(HttpClient httpClient, MESHApiClientConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            _authHeaderGenerator = new AuthenticationHeaderGenerator(config);
        }

        public async Task<GetMessageCountResponse> GetMessageCount()
        {
            var response = await MESHHttpGet($"{_config.BaseUrl}/messageexchange/{_config.MailBoxId}/count");
            return JsonSerializer.Deserialize<GetMessageCountResponse>(await response.Content.ReadAsStringAsync());
        }

        public async Task<GetMessagesResponse> GetMessages()
        {
            var response = await MESHHttpGet($"{_config.BaseUrl}/messageexchange/{_config.MailBoxId}/inbox");
            return JsonSerializer.Deserialize<GetMessagesResponse>(await response.Content.ReadAsStringAsync());
        }

        public async Task<DownloadMessageResponse> DownloadMessage(string messageId)
        {
            // not implemented

            await Authenticate();
            var response = await MESHHttpGet($"{_config.BaseUrl}/messageexchange/{_config.MailBoxId}/inbox/{messageId}");

            var chunkSize = response.Headers.TryGetValues("Mex-Chunk-Size", out var value) ? value.First() : "1:1";

            var currentChunk = chunkSize.Split(':')[0];
            var maxChunk = chunkSize.Split(':')[1];

            return new DownloadMessageResponse();
        }

        public async Task<AcknowledgeMessageResponse> AcknowledgeMessage(string messageId)
        {
            var response = await MESHHttpGet($"{_config.BaseUrl}/messageexchange/{_config.MailBoxId}/inbox/{messageId}/status/acknowledged");
            return JsonSerializer.Deserialize< AcknowledgeMessageResponse >(await response.Content.ReadAsStringAsync());
        }

        private async Task Authenticate()
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{_config.BaseUrl}/messageexchange/{_config.MailBoxId}"),
                Method = HttpMethod.Get
            };

            request.Headers.Authorization = _authHeaderGenerator.GetAuthenticationHeader();

            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Mex-ClientVersion", $"{Constants.ClientName} == 0.0.1");
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Mex-OSArchitecture", "x86_64");
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Mex-OSName", "Windows");
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Mex-OSVersion", "#44~18.04.2-Ubuntu");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        private async Task<HttpResponseMessage> MESHHttpGet(string uri)
        {
            await Authenticate();

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(uri),
                Method = HttpMethod.Get
            };

            request.Headers.Authorization = _authHeaderGenerator.GetAuthenticationHeader();

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return response;
        }
    }
}
