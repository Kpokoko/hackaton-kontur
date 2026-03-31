namespace MockServiceApplication.MockServices;

public interface IMockService
{
    public T Generate<T>(IServiceProvider services, Format format);
}