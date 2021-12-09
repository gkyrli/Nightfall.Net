using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Requests
{
    public abstract class PolicyBase
    {
        [JsonPropertyName("webhookURL")]
        public string WebhookURL { get; internal set; }

        [JsonPropertyName("detectionRuleUUIDs")]
        public HashSet<string> DetectionRuleUUIDs { get; set; }

        [JsonPropertyName("detectionRules")]
        public List<DetectionRule> DetectionRules { get; set; }

        public void AddDetectionRule(params DetectionRule[] rule)
        {
            DetectionRules ??= new List<DetectionRule>();
            DetectionRules.AddRange(rule);
        }

        public void AddDetectionRuleUUIDs(params string[] uuids)
        {
            DetectionRuleUUIDs ??= new HashSet<string>();
            DetectionRuleUUIDs.UnionWith(uuids);
        }
    }
}