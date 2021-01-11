using Newtonsoft.Json;

namespace MESH.Api.Client.Entities
{
    public class GetTrackingInfoResponse
    {
        [JsonProperty("statusSuccess")]
        public bool StatusSuccess { get; set; }

        [JsonProperty("statusCode")]
        public string StatusCode { get; set; }

        [JsonProperty("partnerId")]
        public string PartnerId { get; set; }

        [JsonProperty("checksum")]
        public string Checksum { get; set; }

        [JsonProperty("localId")]
        public string LocalId { get; set; }

        [JsonProperty("dtsId")]
        public string DtsId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("uploadTimestamp")]
        public string UploadTimestamp { get; set; }

        [JsonProperty("workflowId")]
        public string WorkflowId { get; set; }

        [JsonProperty("senderOrgCode")]
        public string SenderOrgCode { get; set; }

        [JsonProperty("statusEvent")]
        public string StatusEvent { get; set; }

        [JsonProperty("processId")]
        public string ProcessId { get; set; }

        [JsonProperty("addressType")]
        public string AddressType { get; set; }

        [JsonProperty("chunkCount")]
        public int ChunkCount { get; set; }

        [JsonProperty("compressFlag")]
        public string CompressFlag { get; set; }

        [JsonProperty("sender")]
        public string Sender { get; set; }

        [JsonProperty("isCompressed")]
        public bool IsCompressed { get; set; }

        [JsonProperty("statusDescription")]
        public string StatusDescription { get; set; }

        [JsonProperty("expiryTime")]
        public string ExpiryTime { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("messageType")]
        public string MessageType { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("encryptedFlag")]
        public string EncryptedFlag { get; set; }

        [JsonProperty("statusTimestamp")]
        public string StatusTimestamp { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }
    }
}
