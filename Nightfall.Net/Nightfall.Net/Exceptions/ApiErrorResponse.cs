using System.Text.Json;
using System.Text.Json.Serialization;
using Nightfall.Net.JsonConverters;

namespace Nightfall.Net.Exceptions
{
    public class ApiErrorResponse
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("additionalData")]
        [JsonConverter(typeof(AnyToString))]
        public string AdditionalData { get; set; }

        public T GetDeserializedAdditionalData<T>(JsonSerializerOptions options = null)
        {
            return JsonSerializer.Deserialize<T>(AdditionalData, options);
        }

        public string Dump()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}