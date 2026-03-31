using System.Text.Json;
using MockServiceApplication.FormatServices;

namespace MockServiceApplication.MockServices;

public class DictionaryMockService : IMockService
{
    private readonly Func<string, IMockService> _formatResolver;

    public DictionaryMockService(Func<string, IMockService> formatResolver)
    {
        _formatResolver = formatResolver;
    }
    public string? Generate(Format? format, Format? formatKey, int? count, string? valueType, string? keyType)
    {
        if (count is null || valueType is null)
            throw new ArgumentException("Не пришёл размер или тип!");

        var dic = new Dictionary<string, string>();
        var mockService = _formatResolver(valueType);

        for (var i = 0; i < count; ++i)
        {
            dic[mockService.Generate(formatKey, null, count, keyType, keyType)] =
                mockService.Generate(format, null, count, valueType, valueType);
        }

        return JsonSerializer.Serialize(dic);
    }
}