using System.Text;

namespace MockServiceApplication.FormatServices;

public class EmailFormatService : IFormatService
{
    public EmailFormatService(Random random)
    {
        Random = random;
    }

    public Random Random { get; }

    public string? Generate()
    {
        const string chars = "abcdefghijklmnopqrstuvwxyZ0123456789";
        var domens = new[] { "@gmail.com", "@mail.ru", "@yandex.ru" };
        var lenght = Random.Next(6, 30);
        var result = new StringBuilder(lenght);

        for (int i = 0; i < lenght; i++)
        {
            result.Append(chars[Random.Next(chars.Length)]);
        }

        result.Append(domens[Random.Next(domens.Length)]);
        
        return result.ToString();
        
    }
}