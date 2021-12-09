using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Requests
{
    public class ContextRule
    {
        
        [JsonPropertyName("regex")]
        public Regex Regex { get; set; }

        [JsonPropertyName("proximity")]
        public Proximity Proximity { get; set; }

        [JsonPropertyName("confidenceAdjustment")]
        public ConfidenceAdjustment ConfidenceAdjustment { get; set; }

        public ContextRule()
        {
        }

        public ContextRule(Regex regex = null, Proximity proximity = null,
            ConfidenceAdjustment confidenceAdjustment = null)
        {
            Regex = regex;
            Proximity = proximity;
            ConfidenceAdjustment = confidenceAdjustment;
        }

        public static ContextRule BuildContextRule(string pattern,bool caseSensitive=default, int windowBefore = 10, int windowAfter = 10,
            ConfidenceEnum confidenceAdjustment = ConfidenceEnum.VERY_LIKELY)
        {
            return new ContextRule(new Regex(pattern, caseSensitive), new Proximity(windowBefore, windowAfter),
                new ConfidenceAdjustment() {FixedConfidence = confidenceAdjustment.ToString()});
        }
    }
}