using System.Text.Json.Serialization;

namespace AvaloniaApplication2;
[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(Person))]
public partial class MyJsonContext : JsonSerializerContext
{
}
