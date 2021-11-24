using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Webhook
{
    public class WebhookRequest
    {
        [JsonPropertyName("findingsURL")]
        public string FindingsURL { get; set; }

        [JsonPropertyName("validUntil")]
        public string ValidUntil { get; set; }

        [JsonPropertyName("uploadID")]
        public string UploadID { get; set; }

        [JsonPropertyName("findingsPresent")]
        public bool FindingsPresent { get; set; }

        [JsonPropertyName("requestMetadata")]
        public string RequestMetadata { get; set; }

        [JsonPropertyName("errors")]
        public List<object> Errors { get; set; }

        [JsonPropertyName("challenge")]
        public string Challenge { get; set; }

        public bool IsChallengeRequest()
        {
            return !string.IsNullOrEmpty(Challenge);
        }

        
        
    }
}