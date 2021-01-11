using System.Text.Json.Serialization;

namespace MESH.Api.Client.Entities
{
    public class GetMessageCountResponse
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("internalId")]
        public string InternalId { get; set; }

        [JsonPropertyName("allResultsIncluded")]
        public bool AllResultsIncluded { get; set; }
    }
}
