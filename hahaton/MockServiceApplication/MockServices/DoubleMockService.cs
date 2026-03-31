using MockServiceApplication.FormatServices;

namespace MockServiceApplication.MockServices;

public class DoubleMockService : DefaultMockService
{
    public DoubleMockService(Func<Format?, IFormatService> formatResolver) : base(formatResolver)
    {
    }

    public new string? Generate(Format? format, int? count, string? valueType)
    {
        var formatService = _formatResolver(format );
        var formatted = formatService.Generate();
        
        return formatted;
    }
}