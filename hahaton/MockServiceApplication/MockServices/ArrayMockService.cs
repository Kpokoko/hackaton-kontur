using MockServiceApplication.FormatServices;

namespace MockServiceApplication.MockServices;

public class ArrayMockService<T> : IMockService<Array>
{
    private readonly Func<string, IMockService<T>> _formatResolver;

    public ArrayMockService(Func<string, IMockService<T>> formatResolver)
    {
        _formatResolver = formatResolver;
    }
    
    public Array Generate(Format format = default, int count = 0, string? valueType = null)
    {
        if (count == 0 || valueType is null)
            throw new ArgumentException("Не пришёл размер или целевой тип!");

        var arr = new T[count];
        var mockService = _formatResolver(valueType);
        for (var i = 0; i < count; ++i)
            arr[i] = mockService.Generate(format);
        return arr;
    }
}