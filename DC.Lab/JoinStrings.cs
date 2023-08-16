namespace DC.Lab;

public class JoinStrings
{
    public static void Execute()
    {
        var names = File.ReadAllLines(@"Files/names.csv");
        var scores = File.ReadAllLines(@"Files/scores.csv");

        var scoreQuery1 =
            from name in names
            let nameFields = name.Split(',')
            from id in scores
            let scoreFields = id.Split(',')
            where Convert.ToInt32(nameFields[2]) == Convert.ToInt32(scoreFields[0])
            select nameFields[0] + "," + scoreFields[1] + "," + scoreFields[2]
                + "," + scoreFields[3] + "," + scoreFields[4];

        OutputQueryResults(scoreQuery1, "Merge two spreadsheets:");

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static void OutputQueryResults(IEnumerable<string> query, string message)
    {
        Console.WriteLine(Environment.NewLine + message);

        foreach (var item in query)
            Console.WriteLine(item);

        Console.WriteLine($"{query.Count()} total names in list");
    }
}
