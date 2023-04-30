using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Orders.WebApi.Converters
{
    public class DateTimeToUTCConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Debug.Assert(typeof(DateTime) == typeToConvert);
            return DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy'-'MM'-'dd' 'HH':'mm:ss"));
        }
    }
}
