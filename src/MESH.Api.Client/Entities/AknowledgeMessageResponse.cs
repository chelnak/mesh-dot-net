using Newtonsoft.Json;

namespace MESH.Api.Client.Entities
{
    public class AknowledgeMessageResponse
    {
        [JsonProperty("messageID")]
        public string Id { get; set; }
    }
}
