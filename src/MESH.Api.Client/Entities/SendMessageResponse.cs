using Newtonsoft.Json;

namespace MESH.Api.Client.Entities
{
    public class SendMessageResponse
    {
        [JsonProperty("messageId")]
        public string MessageId { get; set; }
    }
}
