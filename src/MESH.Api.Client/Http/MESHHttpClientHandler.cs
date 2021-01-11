using MESH.Api.Client.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace MESH.Api.Client.Http
{
    public class MESHHttpClientHandler: HttpClientHandler
    {
        private readonly HttpClientHandler _handler;

        public MESHHttpClientHandler(MESHApiClientConfiguration config)
        {
            _handler = new HttpClientHandler();

            _handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            _handler.ClientCertificates.Add(GetCertificate(config.CertificateThumbprint));

            if (!config.CertificateValidation)
            {
                _handler.ServerCertificateCustomValidationCallback = (m, c, ch, e) => true;
            }

        }

        private static X509Certificate2 GetCertificate(string thumbprint)
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
