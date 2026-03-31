namespace MockServiceApplication.FormatServices;

public class DoubleFormatService : IFormatService
{
    public Random Random { get; }

    public DoubleFormatService(Random random)
    {
        Random = random;
    }

    public string Generate()
    {
        int value = Random.Next();
        double result = Random.NextDouble() * value;
        return result.ToString();
    }
}