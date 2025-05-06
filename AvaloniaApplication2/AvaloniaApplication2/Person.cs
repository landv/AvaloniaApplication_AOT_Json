namespace AvaloniaApplication2;

using System.Text.Json.Serialization;

public class Person
{
    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Age")]
    public long Age { get; set; }

    [JsonPropertyName("Gender")]
    public string Gender { get; set; }

    [JsonPropertyName("Email")]
    public string Email { get; set; }

    [JsonPropertyName("Phone")]
    public string Phone { get; set; }

    [JsonPropertyName("Address")]
    public string Address { get; set; }
}
