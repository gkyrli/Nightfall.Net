using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Requests
{
    /// <summary>
    /// Provides a configuration to let the api know how to mask the finding.
    /// </summary>
    public class MaskConfig
    {
        [JsonPropertyName("maskingChar")]
        public string MaskingChar { get; set; }

        [JsonPropertyName("charsToIgnore")]
        public List<string> CharsToIgnore { get; set; }

        [JsonPropertyName("numCharsToLeaveUnmasked")]
        public int NumCharsToLeaveUnmasked { get; set; }

        [JsonPropertyName("maskLeftToRight")]
        public bool MaskLeftToRight { get; set; }

        public MaskConfig()
        {
        }

        public MaskConfig(string maskingChar, List<string> charsToIgnore, int numCharsToLeaveUnmasked,
            bool maskLeftToRight)
        {
            MaskingChar = maskingChar;
            CharsToIgnore = charsToIgnore;
            NumCharsToLeaveUnmasked = numCharsToLeaveUnmasked;
            MaskLeftToRight = maskLeftToRight;
        }
    }
}