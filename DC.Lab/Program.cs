using System.Text;

class Program
{
    // Demonstrates generic constraint limitation in terms of equality
    // This operation can only perform reference equality, not value equality.
    public static void OpEqualsTest<T>(T s, T t) where T : class
    {
        Console.WriteLine(s == t);
    }

    // Demonstrates an application of using enum constraints
    public static Dictionary<int, string> EnumNamedValues<T>() where T : Enum
    {
        var result = new Dictionary<int, string>();
        var values = Enum.GetValues(typeof(T));

        foreach (int item in values)
            result.Add(item, Enum.GetName(typeof(T), item)!);

        return result;
    }

    enum Rainbow
    {
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        Indigo,
        Violet
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Generic constraint equality limitation:");

        string s1 = "target";
        StringBuilder sb = new("target");
        string s2 = sb.ToString();
        OpEqualsTest<string>(s1, s2);

        Console.WriteLine("\nEnum constraint:");

        var map = EnumNamedValues<Rainbow>();

        foreach (var pair in map)
            Console.WriteLine($"{pair.Key}:\t{pair.Value}");
    }
}