using System.Text;

namespace MockServiceApplication.FormatServices;

public class DefaultFormatService : IFormatService
{
    public Random Random { get; }
    
    public DefaultFormatService(Random random)
    {
        Random = random;
    }
    
    public string? Generate()
    {
        var result = new StringBuilder();
        const string chars = "abcdefghijklmnopqrstuvwxyZ0123456789";
        
        for (var i = 0; i < Random.Next(1, 30); i++)
        {
            result.Append(chars[Random.Next(chars.Length)]);
        }
        
        return result.ToString();
    }
}