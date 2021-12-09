using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Requests
{
    public abstract class ScanBase
    {
        [JsonPropertyName("config")]
        public virtual PolicyBase Config { get; set; }

        public void AddDetectionRules(params DetectionRule[] rule) => Config.AddDetectionRule(rule);
        public void AddDetectionRuleUUids(params string[] uuids) => Config.AddDetectionRuleUUIDs(uuids);

        protected ScanBase(PolicyBase config)
        {
            Config = config;
        }
    }
}