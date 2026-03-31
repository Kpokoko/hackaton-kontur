using System.Text.Json.Serialization;

namespace Domain;

public record Structure(
    string Name,
    string Type,
    [property: JsonConverter(typeof(JsonStringEnumConverter))]Format? Format);
    
    