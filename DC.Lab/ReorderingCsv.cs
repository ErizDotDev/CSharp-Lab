namespace DC.Lab;

public class ReorderingCsv
{
    public static void Execute()
    {
        var lines = File.ReadAllLines(@"Files/spreadsheet1.csv");

        var query =
            from line in lines
            let x = line.Split(',')
            orderby x[2]
            select x[2] + ", " + (x[1] + " " + x[0]);

        File.WriteAllLines(@"Files/spreadsheet2.csv", query.ToArray());

        Console.WriteLine("Spreadsheet2.csv written to disk. Press any key to exit.");
        Console.ReadKey();
    }
}
