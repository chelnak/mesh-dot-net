namespace MESH.Api.Client.Entities
{
    public class GetTrackingInfoResponse
    {
        public bool statusSuccess { get; set; }
        public string statusCode { get; set; }
        public string partnerId { get; set; }
        public string checksum { get; set; }
        public string localId { get; set; }
        public string dtsId { get; set; }
        public string status { get; set; }
        public string uploadTimestamp { get; set; }
        public string workflowId { get; set; }
        public string senderOrgCode { get; set; }
        public string statusEvent { get; set; }
        public string processId { get; set; }
        public string addressType { get; set; }
        public int chunkCount { get; set; }
        public string compressFlag { get; set; }
        public string sender { get; set; }
        public bool isCompressed { get; set; }
        public string statusDescription { get; set; }
        public string expiryTime { get; set; }
        public string version { get; set; }
        public string messageType { get; set; }
        public string subject { get; set; }
        public string encryptedFlag { get; set; }
        public string statusTimestamp { get; set; }
        public string fileName { get; set; }
        public string recipient { get; set; }
    }
}
