using MockServiceApplication.FormatServices;

namespace MockServiceApplication.MockServices;

public class StringMockService : DefaultMockService
{
    private readonly Func<Format?, IFormatService> _formatResolver;

    public StringMockService(Func<Format?, IFormatService> formatResolver) : base(formatResolver)
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