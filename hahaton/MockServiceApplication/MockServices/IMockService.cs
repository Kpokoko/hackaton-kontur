using MockServiceApplication.FormatServices;

namespace MockServiceApplication.MockServices;

public interface IMockService
{
    public string? Generate(Format? format, Format? formatKey, int? count, string? valueType, string? keyType);
}