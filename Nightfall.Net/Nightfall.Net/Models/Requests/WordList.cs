using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Requests
{
    public class WordList
    {
        [JsonPropertyName("values")]
        public ICollection<string> Values { get; set; }

        [JsonPropertyName("isCaseSensitive")]
        public bool IsCaseSensitive { get; set; }

        public WordList()
        {
        }

        public WordList(ICollection<string> values = null, bool isCaseSensitive = default)
        {
            Values = values;
            IsCaseSensitive = isCaseSensitive;
        }
        public WordList(string[] values = null, bool isCaseSensitive = default)
        {
            Values = values.ToList();
            IsCaseSensitive = isCaseSensitive;
        }
    }
}