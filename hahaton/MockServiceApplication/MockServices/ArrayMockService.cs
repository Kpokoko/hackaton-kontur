using MockServiceApplication.FormatServices;

namespace MockServiceApplication.MockServices;

public class ArrayMockService : IMockCollectionService
{
    private readonly Func<string, IMockService> _formatResolver;

    public ArrayMockService(Func<string, IMockService> formatResolver)
    {
        _formatResolver = formatResolver;
    }
    
    public string[] Generate<T>(Format format = default, int count = 0, string? valueType = null)
    {
        if (count == 0 || valueType is null)
            throw new ArgumentException("Не пришёл размер или целевой тип!");

        var arr = new string[count];
        var mockService = _formatResolver(valueType);
        for (var i = 0; i < count; ++i)
            arr[i] = mockService.Generate<T>(format);
        return arr;
    }
}