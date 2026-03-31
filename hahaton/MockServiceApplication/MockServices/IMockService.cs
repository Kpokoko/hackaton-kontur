using MockServiceApplication.FormatServices;

namespace MockServiceApplication.MockServices;

public interface IMockService
{
    public string? Generate(Format? format);
}