using MockServiceApplication.FormatServices;

namespace MockServiceApplication.MockServices;

public interface IMockService<T>
{
    public T Generate(Format format = default, int count = 0, string? valueType = null);
}