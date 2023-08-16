namespace DC.Lab;

public class SortLines
{
    public static void Execute()
    {
        var scores = File.ReadAllLines(@"Files/scores.csv");
        int sortField = 2;

        Console.WriteLine($"Sorted highest to lowest by field: [{sortField}]");

        foreach (var str in RunQuery(scores, sortField))
            Console.WriteLine(str);

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static IEnumerable<string> RunQuery(IEnumerable<string> source, int num)
    {
        var scoreQuery = from line in source
                         let fields = line.Split(',')
                         orderby fields[num] descending
                         select line;

        return scoreQuery;
    }
}
