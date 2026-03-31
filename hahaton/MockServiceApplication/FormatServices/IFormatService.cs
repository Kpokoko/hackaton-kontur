namespace MockServiceApplication.FormatServices;

public interface IFormatService
{
    public Random Random { get; }
    public string? Generate();
}