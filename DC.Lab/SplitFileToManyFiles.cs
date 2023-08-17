namespace DC.Lab;

public class SplitFileToManyFiles
{
    public static void Execute()
    {
        var fileA = File.ReadAllLines(@"Files/names2.txt");
        var fileB = File.ReadAllLines(@"Files/names3.txt");

        var mergeQuery = fileA.Union(fileB);

        var groupQuery = from name in mergeQuery
                         let n = name.Split(',')
                         group name by n[0][0] into g
                         orderby g.Key
                         select g;

        // Create a new file for each group that was created
        // Note that nested foreach loops are required to access
        // individual items with each group.
        foreach (var g in groupQuery)
        {
            string fileName = $"Files/testFile_{g.Key}.txt";

            Console.WriteLine(g.Key);

            using (var sw = new StreamWriter(fileName))
            {
                foreach (var item in g)
                {
                    sw.WriteLine(item);
                    Console.WriteLine($"    {item}");
                }
            }
        }

        Console.WriteLine("Files have been written. Press any key to exit.");
        Console.ReadKey();
    }
}
