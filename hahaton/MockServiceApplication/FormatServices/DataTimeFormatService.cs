namespace MockServiceApplication.FormatServices;

public class DataTimeFormatService : IFormatService<DateTime>
{
    public Random Random { get; }
    public DataTimeFormatService(Random random)
    {
        Random = random;
    }
    public DateTime Generate()
    {
        int year = Random.Next(1990, 2026);
        int mounth = Random.Next(1, 12);
        int day = Random.Next(1, 28);
        
        return new DateTime(year, mounth, day);
    }
}