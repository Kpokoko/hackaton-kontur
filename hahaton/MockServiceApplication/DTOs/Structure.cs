using System.Text.Json.Serialization;
using MockServiceApplication.FormatServices;

namespace MockServiceApplication.DTOs;

public record Structure(
    string Name,
    string Type,
    [property: JsonConverter(typeof(JsonStringEnumConverter))]Format? Format, int? Count, string? valueType);