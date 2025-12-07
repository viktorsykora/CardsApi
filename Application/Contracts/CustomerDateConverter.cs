using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Contracts
{
    public class CustomDateConverter : JsonConverter<DateTime>
    {
        public static readonly string Format = "dd.MM.yyyy";

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format));
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), Format, null);
        }
    }
}