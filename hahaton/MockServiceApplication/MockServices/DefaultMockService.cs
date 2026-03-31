using MockServiceApplication.FormatServices;

namespace MockServiceApplication.MockServices;

public class DefaultMockService : IMockService
{
    protected readonly Func<Format?, IFormatService> _formatResolver;

    public DefaultMockService(Func<Format?, IFormatService> formatResolver)
    {
        _formatResolver = formatResolver;
    }

    public string? Generate(Format? format,  Format? formatKey, int? count, string? valueType, string? keyType)
    {
        var formatService = _formatResolver(format);
        var formatted = formatService.Generate();
        
        return formatted;
    }
}