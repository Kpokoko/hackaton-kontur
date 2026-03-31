using MockServiceApplication.FormatServices;

namespace MockServiceApplication.MockServices;

public class StringMockService : IMockService
{
    private readonly Func<Format?, IFormatService> _formatResolver;

    public StringMockService(Func<Format?, IFormatService> formatResolver)
    {
        _formatResolver = formatResolver;
    }

    public string? Generate(Format? format, int? count, string? valueType)
    {
        var formatService = _formatResolver(format);
        var formatted = formatService.Generate();
        
        return formatted;
    }
}