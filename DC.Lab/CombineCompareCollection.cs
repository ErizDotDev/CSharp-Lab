namespace DC.Lab;

public class CombineCompareCollection
{
    public static void Execute()
    {
        var fileA = File.ReadAllLines(@"Files/names2.txt");
        var fileB = File.ReadAllLines(@"Files/names3.txt");

        var concatQuery = fileA.Concat(fileB).OrderBy(s => s);

        OutputQueryResults(concatQuery, "Simple concatenate and sort. Duplicates are preserved:");

        var uniqueNamesQuery = fileA.Union(fileB).OrderBy(s => s);

        OutputQueryResults(uniqueNamesQuery, "Unique removes duplicate names:");

        var commonNamesQuery = fileA.Intersect(fileB);

        OutputQueryResults(commonNamesQuery, "Merge based on intersect:");

        // Find the matching fields in each list. Merge the two
        // results by using Concat, and then
        // sort using the default string comparer.
        string nameMatch = "Garcia";

        var tempQuery1 =
            from name in fileA
            let n = name.Split(',')
            where n[0] == nameMatch
            select name;

        var tempQuery2 =
            from name2 in fileB
            let n2 = name2.Split(',')
            where n2[0] == nameMatch
            select name2;

        var nameMatchQuery = tempQuery1.Concat(tempQuery2).OrderBy(s => s);

        OutputQueryResults(nameMatchQuery, $"Concat based on partial name match \"{nameMatch}\"");

        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
    }

    static void OutputQueryResults(IEnumerable<string> query, string message)
    {
        Console.WriteLine(Environment.NewLine + message);

        foreach (string item in query)
            Console.WriteLine(item);

        Console.WriteLine($"{query.Count()} total names in list");
    }
}
