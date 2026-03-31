using System.Text.Json;
using MockServiceApplication.FormatServices;

namespace MockServiceApplication.MockServices;

public class ArrayMockService : IMockService
{
    private readonly Func<string, IMockService> _formatResolver;

    public ArrayMockService(Func<string, IMockService> formatResolver)
    {
        _formatResolver = formatResolver;
    }

    public string? Generate(Format? format,  Format? formatKey, int? count, string? valueType, string? keyType)
    {
        if (count is null || valueType is null)
            throw new ArgumentException("Не пришёл размер или тип!");

        var arr = new string[(int)count];
        var mockService = _formatResolver(valueType);
        for (var i = 0; i < count; ++i)
            arr[i] = mockService.Generate(format, format, count, valueType, null);
        return string.Join(",", arr);
    }
}