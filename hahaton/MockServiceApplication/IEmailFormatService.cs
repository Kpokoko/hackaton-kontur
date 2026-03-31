using System.Text;
using MockServiceApplication.FormatServices;

namespace MockServiceApplication;

public class EmailFormatService : IFormatService<string>
{
    public Random Random { get; }

    public string Generate()
    {
        Random random = new Random();
        
        const string chars = "abcdefghijklmnopqrstuvwxyZ0123456789";
        string[] domens = new[] { "@gmail.com", "@mail.ru", "@yandex.ru" };
        int lenght = random.Next(6, 30);
        StringBuilder result = new StringBuilder(lenght);

        for (int i = 0; i < lenght; i++)
        {
            result.Append(chars[random.Next(chars.Length)]);
        }

        result.Append(domens[random.Next(domens.Length)]);
        return result.ToString();
    }
}