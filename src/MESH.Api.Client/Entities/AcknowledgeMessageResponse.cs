using Newtonsoft.Json;

namespace MESH.Api.Client.Entities
{
    public class AcknowledgeMessageResponse
    {
        [JsonProperty("messageID")]
        public string Id { get; set; }
    }
}
