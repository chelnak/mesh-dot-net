using Newtonsoft.Json;
using System.Collections.Generic;

namespace MESH.Api.Client.Entities
{
    public class GetMessagesResponse
    {
        [JsonProperty("messages")]
        public List<string> Messages { get; set; }
    }
}
