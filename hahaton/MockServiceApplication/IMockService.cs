namespace MockServiceApplication;

public interface IMockService
{
    public IEnumerable<T> Generate<T>(int count, T targetObject, Format format)
    {
        throw new NotImplementedException();
    }
}