namespace DC.Lab;

public class ProjectionOperation
{
    public static void Execute()
    {
        DemoSimpleZip();
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

        foreach ((char letter, int number) in letters.Zip(numbers))
        {
            Console.WriteLine($"Number {number} zipped with letter {letter}");
        }
    }
}
