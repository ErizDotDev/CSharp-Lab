namespace DC.Lab;

public class ComputeColumns
{
    public static void Execute()
    {
        var lines = File.ReadAllLines("Files/scores.csv");

        // Column that will be computed
        int exam = 3;

        SingleColumn(lines, exam + 1);
        Console.WriteLine();
        MultiColumns(lines);

        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
    }

    public static void SingleColumn(IEnumerable<string> strs, int examNum)
    {
        Console.WriteLine("Single column query:");

        var columnQuery =
            from line in strs
            let elements = line.Split(',')
            select Convert.ToInt32(elements[examNum]);

        var results = columnQuery.ToList();

        double average = results.Average();
        int max = results.Max();
        int min = results.Min();

        Console.WriteLine($"Exam #{examNum}: Average:{average:##.##} High Score: {max} Low Score: {min}");
    }

    static void MultiColumns(IEnumerable<string> strs)
    {
        Console.WriteLine("Multi Column Query:");

        // Create a query, multiColQuery. Explicit typing is used
        // to make clear that, when executed, multiColQuery produces
        // nested sequences. However, you get the same results by
        // using 'var'.

        var multiColQuery =
            from line in strs
            let elements = line.Split(',')
            let scores = elements.Skip(1)
            select (from str in scores
                    select Convert.ToInt32(str));

        // Execute the query and cache the results to improve
        // performance.
        // ToArray could be used instead of ToList.
        var results = multiColQuery.ToList();

        // Find out how many columns you have in results.
        int columnCount = results[0].Count();

        // Perform aggregate calculations Average, Max, and
        // Min on each column.
        // Perform one iteration of the loop for each column
        // of scores.
        // You can use a for loop instead of a foreach loop
        // because you already executed the multiColQuery
        // query by calling ToList.
        for (int column = 0; column < columnCount; column++)
        {
            var results2 = from row in results
                           select row.ElementAt(column);
            double average = results2.Average();
            int max = results2.Max();
            int min = results2.Min();

            Console.WriteLine($"Exam #{column + 1}: Average:{average:##.##} High Score: {max} Low Score: {min}");
        }
    }
}
