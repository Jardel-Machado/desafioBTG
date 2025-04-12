using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DesafioBtg.Dominio.Uteis;

public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    private const string Formato = "dd/MM/yyyy HH:mm:ss";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.ParseExact(reader.GetString(), Formato, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Formato));
    }
}
