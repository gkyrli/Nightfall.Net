using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Requests
{
    public class Proximity
    {
        [JsonPropertyName("windowBefore")]
        public int WindowBefore { get; set; }

        [JsonPropertyName("windowAfter")]
        public int WindowAfter { get; set; }

        public Proximity()
        {
        }

        public Proximity(int windowBefore = 0, int windowAfter = 0)
        {
            WindowBefore = windowBefore;
            WindowAfter = windowAfter;
        }
    }
}