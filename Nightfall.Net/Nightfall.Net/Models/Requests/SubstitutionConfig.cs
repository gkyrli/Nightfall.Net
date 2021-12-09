using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Requests
{
    /// <summary>
    /// Provide a string that is going to be used to replace the finding
    /// </summary>
    public class SubstitutionConfig
    {
        [JsonPropertyName("substitutionPhrase")]
        public string SubstitutionPhrase { get; set; }

        public SubstitutionConfig()
        {
        }

        public SubstitutionConfig(string substitutionPhrase = null)
        {
            SubstitutionPhrase = substitutionPhrase;
        }
    }
}