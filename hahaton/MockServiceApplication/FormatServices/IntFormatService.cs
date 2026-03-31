namespace MockServiceApplication.FormatServices;

public class IntFormatService : IFormatService
{
    public Random Random { get; }

    public IntFormatService(Random random)
    {
        Random = random;
    }

    public string Generate()
    {
        return Random.Next().ToString();
    }
}