using System.Text.Json.Serialization;

namespace MESH.Api.Client.Entities
{
    public class SendMessageResponse
    {
        [JsonPropertyName("messageId")]
        public string MessageId { get; set; }
    }
}
