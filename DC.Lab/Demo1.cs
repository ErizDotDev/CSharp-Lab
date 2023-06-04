namespace DC.Lab;

internal class Demo1
{
    public static void Execute()
    {
        string[] words = new string[]
        {
            "The",
            "quick",
            "brown",
            "fox",
            "jump",
            "over",
            "the",
            "lazy",
            "dog"
        };

        Console.WriteLine($"The last word is {words[^1]}");

        string[] quickBrownFox = words[1..4];
        PrintWords(quickBrownFox);

        string[] lazyDog = words[^2..^0];
        PrintWords(lazyDog);

        string[] allWords = words[..];
        string[] firstPhrase = words[..4];
        string[] lastPhrase = words[6..];

        PrintWords(allWords);
        PrintWords(firstPhrase);
        PrintWords(lastPhrase);

        Index the = ^3;
        Console.WriteLine(words[the]);

        Range phrase = 1..4;
        string[] text = words[phrase];

        PrintWords(text);
    }

    static void PrintWords(string[] words)
    {
        foreach (var word in words)
            Console.WriteLine($"< {word} >");

        Console.WriteLine();

    }
}
