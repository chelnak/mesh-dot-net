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
        private readonly IDateTimeNowProvider _dateTime;
        private readonly IGuidProvider _guid;

        public AuthenticationHeaderGenerator(MESHApiClientConfiguration config)
        {
            _config = config;
            _dateTime = new DateTimeNowProvider();
            _guid = new GuidProvider();
        }

        public AuthenticationHeaderGenerator(MESHApiClientConfiguration config, IDateTimeNowProvider dateTimeNowProvider, IGuidProvider guidProvider)
        {
            _config = config;
            _dateTime = dateTimeNowProvider;
            _guid = guidProvider;
        }

        public AuthenticationHeaderValue GetAuthenticationHeader()
        {
            var mailboxId = _config.MailBoxId.ToUpper();
            var nonce = _guid.NewGuid().ToString();
            var timestamp = _dateTime.UtcNow().ToString("yyyyMMddHHmm");

            var hashContent = $"{mailboxId}:{nonce}:001:{_config.MailBoxPassword}:{timestamp}";
            var hash = Hash(hashContent, _config.SharedKey);

            return new AuthenticationHeaderValue(Constants.AuthenticationScheme, $"{mailboxId}:{nonce}:001:{timestamp}:{hash}");
        }

        private string Hash(string message, string secret)
        {
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
