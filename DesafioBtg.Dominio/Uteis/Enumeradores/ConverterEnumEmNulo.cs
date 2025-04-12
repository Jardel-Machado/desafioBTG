using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DesafioBtg.Dominio.Uteis.Enumeradores;

[ExcludeFromCodeCoverage]
public class ConverterEnumEmNulo<T> : JsonConverter<T?> where T : struct, Enum
{
    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string value = reader.GetString();

        if (string.IsNullOrEmpty(value))        
            return null;        

        return Enum.Parse<T>(value, true);
    }

    public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
    {
        if (value.HasValue)        
            writer.WriteStringValue(value.ToString());        
        else        
            writer.WriteNullValue();        
    }
}
