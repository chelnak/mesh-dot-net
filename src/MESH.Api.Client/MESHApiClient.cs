using MESH.Api.Client.Authentication;
using MESH.Api.Client.Configuration;
using MESH.Api.Client.Entities;
using Newtonsoft.Json;
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

        public MESHApiClient(HttpClient httpClient, MESHApiClientConfiguration config, IAuthenticationHeaderGenerator authHeaderGenerator)
        {
            _httpClient = httpClient;
            _config = config;
            _authHeaderGenerator = authHeaderGenerator;
        }

        public async Task<GetMessageCountResponse> GetMessageCount()
        {
            await Authenticate();
            var response = await MESHHttpGet($"{_config.BaseUrl}/messageexchange/{_config.MailBoxId}/count");
            return JsonConvert.DeserializeObject<GetMessageCountResponse>(await response.Content.ReadAsStringAsync());
        }

        public async Task<GetMessagesResponse> GetMessages()
        {
            await Authenticate();
            var response = await MESHHttpGet($"{_config.BaseUrl}/messageexchange/{_config.MailBoxId}/inbox");
            return JsonConvert.DeserializeObject<GetMessagesResponse>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Message> DownloadMessage(string messageId)
        {
            // not implemented

            await Authenticate();
            var response = await MESHHttpGet($"{_config.BaseUrl}/messageexchange/{_config.MailBoxId}/inbox/{messageId}");

            var chunkSize = response.Headers.TryGetValues("Mex-Chunk-Size", out var value) ? value.First() : "1:1";

            var currentChunk = chunkSize.Split(':')[0];
            var maxChunk = chunkSize.Split(':')[1];

            return new Message();
        }

        public async Task<AknowledgeMessageResponse> AknowledgeMessage(string messageId)
        {
            await Authenticate();
            var response = await MESHHttpGet($"{_config.BaseUrl}/messageexchange/{_config.MailBoxId}/inbux/{messageId}/status/acknowledged");
            return JsonConvert.DeserializeObject<AknowledgeMessageResponse>(await response.Content.ReadAsStringAsync());
        }

        private async Task Authenticate()
        {
            await MESHHttpGet($"{_config.BaseUrl}/messageexchange/{_config.MailBoxId}");
        }

        private async Task<HttpResponseMessage> MESHHttpGet(string uri)
        {
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
