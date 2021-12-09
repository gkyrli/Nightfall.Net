using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nightfall.Net.JsonConverters
{
    public class AnyToString:JsonConverter<string>
    {
        public override string Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            return Encoding.UTF8.GetString(reader.ValueSpan);
        }

        public override void Write(
            Utf8JsonWriter writer,
            string value,
            JsonSerializerOptions options) =>
            writer.WriteStringValue(value);
    }
}