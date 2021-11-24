using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Requests
{
    public class Policy : PolicyBase
    {
        

        public Policy()
        {
        }

        public Policy(string webhookUrl = null)
        {
            WebhookURL = webhookUrl;
        }
    }

    public class ScanUploadedRequest : ScanBase
    {
        [JsonPropertyName("policyUUID")]
        public string PolicyUUID { get; set; }

        [JsonPropertyName("requestMetadata")]
        public string RequestMetadata { get; set; }

        [JsonPropertyName("policy")]
        public override PolicyBase Config { get; set; }

        public ScanUploadedRequest(string webhook = null, string policyUuid = null, string requestMetadata = null) :
            base(new Policy(webhook))
        {
            PolicyUUID = policyUuid;
            RequestMetadata = requestMetadata;
        }
    }
}