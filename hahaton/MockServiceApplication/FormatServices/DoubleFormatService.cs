namespace MockServiceApplication.FormatServices;

public class DoubleFormatService : IFormatService<double>
{
    public Random Random { get; }

    public DoubleFormatService(Random random)
    {
        Random = random;
    }

    public double Generate()
    {
        int value = Random.Next();
        double result = Random.NextDouble() * value;
        return result;
    }
}