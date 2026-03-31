using MockServiceApplication.FormatServices;

namespace MockServiceApplication.MockServices;

public class DefaultMockService : IMockService
{
    protected readonly Func<Format?, IFormatService> _formatResolver;

    public DefaultMockService(Func<Format?, IFormatService> formatResolver)
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