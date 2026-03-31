using MockServiceApplication.FormatServices;

namespace MockServiceApplication.MockServices;

public class IntMockService : DefaultMockService
{
    
    public IntMockService(Func<Format?, IFormatService> formatResolver) : base(formatResolver)
    {
    }

    public new string? Generate(Format? format, int? count, string? valueType)
    {
        var formatService = _formatResolver(format );
        var formatted = formatService.Generate();
        
        return formatted;
    }
}