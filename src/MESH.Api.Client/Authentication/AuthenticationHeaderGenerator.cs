using MESH.Api.Client.Configuration;
using System;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace MESH.Api.Client.Authentication
{
    public class AuthenticationHeaderGenerator : IAuthenticationHeaderGenerator
    {
        private readonly MESHApiClientConfiguration _config;

        public AuthenticationHeaderGenerator(MESHApiClientConfiguration config)
        {
            _config = config;
        }

        public AuthenticationHeaderValue GetAuthenticationHeader()
        {
            /*
             * 'NHSMESH' - The name of the Custom Authentication Schema
             *  Mailbox ID - ID of the Mailbox sending the HTTP Request, must be uppercase.
             *  Nonce - A GUID used as an encryption 'Nonce'
             *  NonceCount - The number of times that the same 'Nonce' has been used.
             *  Timestamp - The current date and time in 'yyyyMMddHHmm' format.
             *  Hash Code - HMAC-SHA256 Hash Code generated from the concatenation of the following elements, joined by ':'
             *     Mailbox ID - As above
             *     Nonce - As above
             *     Nonce Count - As above
             *     Mailbox Password - The password for the MESH Mailbox.
             *     Timestamp - As above
             *
             *  Example Token: "NHSMESH MAILBOX01:73eefd69-811f-44d0-81f8-a54ff352a991:001:201511041205:3097fd5aa85a...f540942614b"
             */

            var mailboxId = _config.MailBoxId.ToUpper();
            var nonce = Guid.NewGuid().ToString();
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmm");

            var hashContent = $"{mailboxId}:{nonce}:001:{_config.MailBoxPassword}:{timestamp}";
            var hash = Hash(hashContent, _config.SharedKey);

            return new AuthenticationHeaderValue(_config.AuthenticationScheme, $"{mailboxId}:{nonce}:001:{timestamp}:{hash}");
        }

        private string Hash(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new ASCIIEncoding();
            var keyByte = encoding.GetBytes(secret);
            var messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                var hash = hmacsha256.ComputeHash(messageBytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}
