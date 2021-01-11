using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace MESH.Api.Client.Entities
{
    public class GetMessagesResponse
    {
        [JsonPropertyName("messages")]
        public List<string> Messages { get; set; }
    }
}
