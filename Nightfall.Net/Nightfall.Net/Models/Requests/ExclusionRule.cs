using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Requests
{
    public class ExclusionRule
    {
        [JsonPropertyName("matchType")]
        public string MatchType { get; set; }

        [JsonPropertyName("exclusionType")]
        public string ExclusionType { get; set; }

        [JsonPropertyName("regex")]
        public Regex Regex { get; set; }

        [JsonPropertyName("wordList")]
        public WordList WordList { get; set; }

        public ExclusionRule()
        {
        }

        public ExclusionRule(MatchType matchType, Regex regex)
        {
            MatchType = matchType.ToString();
            Regex = regex;
            ExclusionType = "REGEX";
        }

        public ExclusionRule(MatchType matchType, WordList wordList)
        {
            MatchType = matchType.ToString();
            WordList = wordList;
            ExclusionType = "WORD_LIST";
        }
    }
}