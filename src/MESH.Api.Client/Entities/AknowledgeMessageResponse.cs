using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESH.Api.Client.Entities
{
    public class AknowledgeMessageResponse
    {
        [JsonProperty("messageID")]
        public string Id { get; set; }
    }
}
