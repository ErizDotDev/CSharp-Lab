namespace DC.Lab;

public class ProjectionOperation
{
    public static void Execute()
    {
        //DemoSimpleZip();
        DemoSelectVsSelectMany();
    }

    public static void DemoSimpleZip()
    {
        IEnumerable<int> numbers = new[]
        {
            1, 2, 3, 4, 5, 6, 7
        };

        IEnumerable<char> letters = new[]
        {
            'A', 'B', 'C', 'D', 'E', 'F'
        };

        foreach ((int number, char letter) in numbers.Zip(letters))
        {
            Console.WriteLine($"Number {number} zipped with letter {letter}");
        }
    }

    public static void DemoSelectVsSelectMany()
    {
        List<Bouquet> bouquets = new()
        {
            new Bouquet { Flowers = new List<string> { "sunflower", "daisy", "daffodil", "larkspur" } },
            new Bouquet { Flowers = new List<string> { "tulip", "rose", "orchid" }},
            new Bouquet { Flowers = new List<string> { "gladiolis", "lily", "snapdragon", "aster", "protea" }},
            new Bouquet { Flowers = new List<string> { "larkspur", "lilac", "iris", "dahlia" }}
        };

        IEnumerable<List<string>> query1 = bouquets.Select(b => b.Flowers);
        IEnumerable<string> query2 = bouquets.SelectMany(b => b.Flowers);

        Console.WriteLine("Results by using Select():");

        foreach (IEnumerable<string> collection in query1)
            foreach (string item in collection)
                Console.WriteLine(item);

        Console.WriteLine("\nResults by using SelectMany():");

        foreach (string item in query2)
            Console.WriteLine(item);
    }
}

class Bouquet
{
    public List<string> Flowers { get; set; }
}
