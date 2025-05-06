using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Uso: programma <url> [parola]");
            return;
        }

        string url = args[0];
        string? parola = args.Length >= 2 ? args[1] : null;

        string testo = await GetTextFromUrl(url);

        if (!string.IsNullOrEmpty(parola))
        {
            int count = CountOccurrences(testo, parola);
            Console.WriteLine($"La parola '{parola}' appare {count} volta/e.");
        }
        else
        {
            PrintTextStats(testo);
        }
    }

    static async Task<string> GetTextFromUrl(string url)
    {
        using HttpClient client = new HttpClient();
        return await client.GetStringAsync(url);
    }

    static int CountOccurrences(string text, string word)
    {
        var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return words.Count(w => w.Equals(word, StringComparison.OrdinalIgnoreCase));
    }

    static void PrintTextStats(string text)
    {
        int charCount = text.Length;
        int wordCount = text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
        int vowelCount = text.Count(c => "aeiouAEIOU".Contains(c));
        int consonantCount = text.Count(c => Char.IsLetter(c) && !"aeiouAEIOU".Contains(c));

        Console.WriteLine($"Totale caratteri: {charCount}");
        Console.WriteLine($"Totale parole: {wordCount}");
        Console.WriteLine($"Totale vocali: {vowelCount}");
        Console.WriteLine($"Totale consonanti: {consonantCount}");
    }
}

