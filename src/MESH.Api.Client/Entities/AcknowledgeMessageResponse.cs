using System.Text.Json.Serialization;

namespace MESH.Api.Client.Entities
{
    public class AcknowledgeMessageResponse
    {
        [JsonPropertyName("messageID")]
        public string Id { get; set; }
    }
}
