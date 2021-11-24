using System;
using System.Security.Cryptography;
using System.Text;

namespace Nightfall.Net.WebhookSignature
{
    public class WebhookSignatureValidator
    {
        private readonly byte[] secretBytes;

        public WebhookSignatureValidator(string secret)
        {
            secretBytes = Encoding.ASCII.GetBytes(secret);
        }

        public bool Validate(string raw, string timestamp, string signature)
        {
            String payload = timestamp + ":" + raw;
            byte[] hashed = Encoding.UTF8.GetBytes(payload);
            return ToHex(HashHmac(secretBytes, hashed))
                       .ToLowerInvariant()
                   == signature.ToLowerInvariant();
        }
        private string ToHex(byte[]data)
        {
            return BitConverter.ToString(data).Replace("-", string.Empty);
        }
        private static byte[] HashHmac(byte[] key, byte[] message)
        {
            var hash = new HMACSHA256(key);
            return hash.ComputeHash(message);
        }
    }
}