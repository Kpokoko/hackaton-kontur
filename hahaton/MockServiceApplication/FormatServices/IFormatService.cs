namespace MockServiceApplication.FormatServices;

public interface IFormatService<T>
{
    public Random Random { get; }
    public T Generate();
}