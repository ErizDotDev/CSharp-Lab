using System.Text.RegularExpressions;

namespace DC.Lab;

public class QueryWithRegex
{
    public static void Execute()
    {
        string startFolder = @"C:\Program Files (x86)\Microsoft Visual Studio";

        var fileList = GetFiles(startFolder);
        var searchTerm = new Regex(@"Visual (Basic|C#|C\+\+|Studio)");

        // Search the contents of each .htm file
        // Remove the where clause to find even more matchedValues!
        // This query produces a list of files where a match
        // was found, and a list of the matchedValues in that file.
        // Note: Explicit typing of "Match" in select clause.
        // This is required because MatchCollection is not a
        // generic IEnumerable collection
        var queryMatchingFiles =
            from file in fileList
            where file.Extension == ".htm"
            let fileText = File.ReadAllText(file.FullName)
            let matches = searchTerm.Matches(fileText)
            where matches.Count > 0
            select new
            {
                Name = file.FullName,
                matchedValues = from Match match in matches
                                select match.Value
            };

        Console.WriteLine($"The term \"{searchTerm.ToString()}\" was found in:  ");

        foreach (var v in queryMatchingFiles)
        {
            string s = v.Name.Substring(startFolder.Length - 1);
            Console.WriteLine(s);

            // For this file, write out all matching strings
            foreach (var v2 in v.matchedValues)
            {
                Console.WriteLine($" {v2}");
            }
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static IEnumerable<FileInfo> GetFiles(string path)
    {
        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException();

        string[]? fileNames = null;
        List<FileInfo> files = new List<FileInfo>();

        fileNames = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

        foreach (var name in fileNames)
            files.Add(new FileInfo(name));

        return files;
    }
}
