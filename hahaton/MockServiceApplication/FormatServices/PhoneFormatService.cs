namespace MockServiceApplication.FormatServices;

public class PhoneFormatService : IFormatService
{
    public Random Random { get; }

    public PhoneFormatService(Random random)
    {
        this.Random = random;
    }

    public string Generate()
    {
        return $"+7 ({Random.Shared.Next(100, 1000)}) {Random.Shared.Next(100, 1000)}-{Random.Shared.Next(10, 100)}-{Random.Shared.Next(10, 100)}";
    }
}