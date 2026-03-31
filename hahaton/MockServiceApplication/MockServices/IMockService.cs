using MockServiceApplication.FormatServices;

namespace MockServiceApplication.MockServices;

public interface IMockService<T>
{
    public T Generate(Format format);
}