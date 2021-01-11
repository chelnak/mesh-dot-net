using System.Text.Json.Serialization;

namespace MESH.Api.Client.Entities
{
    public class GetTrackingInfoResponse
    {
        [JsonPropertyName("statusSuccess")]
        public bool StatusSuccess { get; set; }

        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; }

        [JsonPropertyName("partnerId")]
        public string PartnerId { get; set; }

        [JsonPropertyName("checksum")]
        public string Checksum { get; set; }

        [JsonPropertyName("localId")]
        public string LocalId { get; set; }

        [JsonPropertyName("dtsId")]
        public string DtsId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("uploadTimestamp")]
        public string UploadTimestamp { get; set; }

        [JsonPropertyName("workflowId")]
        public string WorkflowId { get; set; }

        [JsonPropertyName("senderOrgCode")]
        public string SenderOrgCode { get; set; }

        [JsonPropertyName("statusEvent")]
        public string StatusEvent { get; set; }

        [JsonPropertyName("processId")]
        public string ProcessId { get; set; }

        [JsonPropertyName("addressType")]
        public string AddressType { get; set; }

        [JsonPropertyName("chunkCount")]
        public int ChunkCount { get; set; }

        [JsonPropertyName("compressFlag")]
        public string CompressFlag { get; set; }

        [JsonPropertyName("sender")]
        public string Sender { get; set; }

        [JsonPropertyName("isCompressed")]
        public bool IsCompressed { get; set; }

        [JsonPropertyName("statusDescription")]
        public string StatusDescription { get; set; }

        [JsonPropertyName("expiryTime")]
        public string ExpiryTime { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("messageType")]
        public string MessageType { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("encryptedFlag")]
        public string EncryptedFlag { get; set; }

        [JsonPropertyName("statusTimestamp")]
        public string StatusTimestamp { get; set; }

        [JsonPropertyName("fileName")]
        public string FileName { get; set; }
    }
}
