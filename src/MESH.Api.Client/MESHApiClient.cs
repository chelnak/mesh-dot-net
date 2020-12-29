using MESH.Api.Client.Authentication;
using MESH.Api.Client.Configuration;
using MESH.Api.Client.Entities;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        private async Task Authenticate()
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{_config.BaseUrl}/messageexchange/{_config.MailBoxId}"),
                Method = HttpMethod.Get,
            };

            request.Headers.Authorization = _authHeaderGenerator.GetAuthenticationHeader();

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

        }

        public async Task<GetMessageCountResponse> GetMessageCount()
        {

            await Authenticate();

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{_config.BaseUrl}/messageexchange/{_config.MailBoxId}/count"),
                Method = HttpMethod.Get,
            };

            request.Headers.Authorization = _authHeaderGenerator.GetAuthenticationHeader();

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GetMessageCountResponse>(responseString);
        }

        public async Task<GetMessagesResponse> GetMessages()
        {
            await Authenticate();

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{_config.BaseUrl}/messageexchange/{_config.MailBoxId}/inbox"),
                Method = HttpMethod.Get
            };

            request.Headers.Authorization = _authHeaderGenerator.GetAuthenticationHeader();

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GetMessagesResponse>(responseString);
        }

        public async Task<Message> DownloadMessage(string messageId)
        {

            // not implemented


            await Authenticate();

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{_config.BaseUrl}/messageexchange/{_config.MailBoxId}/inbox/{messageId}"),
                Method = HttpMethod.Get,
            };

            request.Headers.Authorization = _authHeaderGenerator.GetAuthenticationHeader();

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var chunkSize = response.Headers.TryGetValues("Mex-Chunk-Size", out var value) ? value.First() : "1:1";

            var currentChunk = chunkSize.Split(':')[0];
            var maxChunk = chunkSize.Split(':')[1];


            return new Message();
        }


        public async Task<AknowledgeMessageResponse> AknowledgeMessage(string messageId)
        {
            await Authenticate();

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{_config.BaseUrl}/messageexchange/{_config.MailBoxId}/inbux/{messageId}/status/acknowledged"),
                Method = HttpMethod.Put,
                Content = new StringContent("")
            };

            request.Headers.Authorization = _authHeaderGenerator.GetAuthenticationHeader();

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AknowledgeMessageResponse>(responseString);
        }


    }
}



