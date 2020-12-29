using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace MESH.Api.Client
{
    public static class CertificateHelper
    {

        public static X509Certificate2 GetCertificate(string thumbprint)
        {

            using (X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                certStore.Open(OpenFlags.ReadOnly);

                X509Certificate2Collection certCollection = certStore.Certificates.Find(
                                            X509FindType.FindByThumbprint,
                                            thumbprint.ToUpper(),
                                            false);
                // Get the first cert with the thumbprint
                X509Certificate2 cert = (X509Certificate2)certCollection.OfType<X509Certificate>().FirstOrDefault();

                if (cert is null)
                    throw new Exception($"Certificate with thumbprint {thumbprint} was not found");

                return cert;
            }
        }

    }
}
