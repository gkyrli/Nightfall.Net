using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Requests
{
    public class ConfidenceAdjustment
    {
        [JsonPropertyName("fixedConfidence")]
        public string FixedConfidence { get; set; }
    }
}