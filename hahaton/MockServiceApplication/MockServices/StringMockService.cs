using MockServiceApplication.FormatServices;

namespace MockServiceApplication.MockServices;

public class StringMockService : DefaultMockService
{
    public StringMockService(Func<Format?, IFormatService> formatResolver) : base(formatResolver)
    {
    }
}