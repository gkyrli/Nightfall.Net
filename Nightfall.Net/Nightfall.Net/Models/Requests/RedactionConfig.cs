using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Requests
{
    /// <summary>
    /// An object that configures how any detected findings should be redacted when returned to the client. When this
    /// configuration is provided as part of a request, exactly one of the four types of redaction should be set.
    /// </summary>
    public class RedactionConfig
    {
        [JsonPropertyName("maskConfig")]
        public MaskConfig MaskConfig { get; set; }

        [JsonPropertyName("infoTypeSubstitutionConfig")]
        public InfoTypeSubstitutionConfig InfoTypeSubstitutionConfig { get; set; }

        [JsonPropertyName("substitutionConfig")]
        public SubstitutionConfig SubstitutionConfig { get; set; }

        [JsonPropertyName("cryptoConfig")]
        public CryptoConfig CryptoConfig { get; set; }

        [JsonPropertyName("removeFinding")]
        public bool RemoveFinding { get; set; }

        public RedactionConfig()
        {
        }

        public RedactionConfig(MaskConfig maskConfig = null,
            InfoTypeSubstitutionConfig infoTypeSubstitutionConfig = null, SubstitutionConfig substitutionConfig = null,
            CryptoConfig cryptoConfig = null, bool removeFinding = default)
        {
            MaskConfig = maskConfig;
            InfoTypeSubstitutionConfig = infoTypeSubstitutionConfig;
            SubstitutionConfig = substitutionConfig;
            CryptoConfig = cryptoConfig;
            RemoveFinding = removeFinding;
        }

        public RedactionConfig(InfoTypeSubstitutionConfig infoTypeSubstitutionConfig)
        {
            InfoTypeSubstitutionConfig = infoTypeSubstitutionConfig;
        }

        public RedactionConfig(SubstitutionConfig substitutionConfig)
        {
            SubstitutionConfig = substitutionConfig;
        }

        public RedactionConfig(MaskConfig maskConfig)
        {
            MaskConfig = maskConfig;
        }

        public RedactionConfig(CryptoConfig cryptoConfig)
        {
            CryptoConfig = cryptoConfig;
        }
    }
}