using Newtonsoft.Json;

namespace MESH.Api.Client.Entities
{
    public class GetMessageCountResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("internalId")]
        public string InternalId { get; set; }

        [JsonProperty("allResultsIncluded")]
        public bool AllResultsIncluded { get; set; }
    }
}
