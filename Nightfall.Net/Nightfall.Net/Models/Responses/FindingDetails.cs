using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Responses
{

    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Detector
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }
    }

    public class ByteRange
    {
        [JsonPropertyName("start")]
        public int Start { get; set; }

        [JsonPropertyName("end")]
        public int End { get; set; }
    }

    public class CodepointRange
    {
        [JsonPropertyName("start")]
        public int Start { get; set; }

        [JsonPropertyName("end")]
        public int End { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("byteRange")]
        public ByteRange ByteRange { get; set; }

        [JsonPropertyName("codepointRange")]
        public CodepointRange CodepointRange { get; set; }
    }

    public class FindingDetails
    {
        [JsonPropertyName("finding")]
        public string Finding { get; set; }

        [JsonPropertyName("detector")]
        public Detector Detector { get; set; }

        [JsonPropertyName("confidence")]
        public string Confidence { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("matchedDetectionRuleUUIDs")]
        public List<string> MatchedDetectionRuleUUIDs { get; set; }

        [JsonPropertyName("matchedDetectionRules")]
        public List<object> MatchedDetectionRules { get; set; }
    }

    public class ScanResponse
    {
        [JsonPropertyName("findings")]
        public List<List<FindingDetails>> Findings { get; set; }

        [JsonPropertyName("redactedPayload")]
        public List<string> RedactedPayload { get; set; }
    }
}