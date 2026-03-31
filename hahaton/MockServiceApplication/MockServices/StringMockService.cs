using MockServiceApplication.FormatServices;

namespace MockServiceApplication.MockServices;

public class StringMockService : IMockService<string>
{
    private readonly Func<Format, IFormatService<string>> _formatResolver;

    public StringMockService(Func<Format, IFormatService<string>> formatResolver)
    {
        _formatResolver = formatResolver;
    }

    public string Generate(Format format, int count = 0, string? valueType = null)
    {
        var formatService = _formatResolver(format);
        var formatted = formatService.Generate();
        
        return formatted;
    }
}