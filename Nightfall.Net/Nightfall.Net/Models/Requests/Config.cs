using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Requests
{
    public class Config : PolicyBase
    {
        public Config(int contextBytes) => ContextBytes = contextBytes;

        [JsonPropertyName("contextBytes")]
        public int ContextBytes { get; set; }
    }
}