using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Requests
{
    public class Regex
    {
        [JsonPropertyName("pattern")]
        public string Pattern { get; set; }

        [JsonPropertyName("isCaseSensitive")]
        public bool IsCaseSensitive { get; set; }

        public Regex()
        {
        }

        public Regex(string pattern = null, bool isCaseSensitive = default)
        {
            Pattern = pattern;
            IsCaseSensitive = isCaseSensitive;
        }
    }
}