using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Requests
{
    public class ScanRequestConfig : ScanBase
    {
        [JsonPropertyName("payload")]
        public List<string> Payload { get; set; } = new List<string>();

        /// <param name="contextBytes">Maximum value is 40</param>
        public ScanRequestConfig(int? contextBytes = null) : base(new Config(contextBytes ?? 0))
        {
        }

        public void AddPayload(params string[] value)
        {
            Payload.AddRange(value);
        }
    }
}