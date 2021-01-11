namespace MESH.Api.Client.Configuration
{
    public class MESHApiClientConfiguration
    {
        public string BaseUrl { get; set; }
        public string MailBoxId { get; set; }
        public string MailBoxPassword { get; set; }
        public string SharedKey { get; set; }
        public string CertificateThumbprint { get; set; }
        public bool CertificateValidation {get; set;}
    }
}
