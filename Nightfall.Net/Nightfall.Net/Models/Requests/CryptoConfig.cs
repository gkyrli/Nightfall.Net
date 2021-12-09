using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Requests
{
    public class CryptoConfig
    {
        [JsonPropertyName("publicKey")]
        public string PublicKey { get; set; }

        public CryptoConfig(string publicKey)
        {
            PublicKey = publicKey;
        }
    }
}