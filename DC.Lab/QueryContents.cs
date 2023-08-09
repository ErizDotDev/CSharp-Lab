namespace DC.Lab;

public class QueryContents
{
    public static void Execute()
    {
        var startFolder = @"C:\Program Files\Microsoft Visual Studio\2022\Community\Common7";
        var dir = new DirectoryInfo(startFolder);
        var fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);

        string searchTerm = @"Visual Studio";

        // Search the contents of the file.
        // A regular expression created with the RegEx class
        // could be used instead of the Contains method.
        // queryMatchingFiles is an IEnumerable<string>.
        var queryMatchingFiles =
            from file in fileList
            where file.Extension == ".htm"
            let fileText = GetFileText(file.FullName)
            where fileText.Contains(searchTerm)
            select file.FullName;

        Console.WriteLine($"The term \"{searchTerm}\" was found in:");

        foreach (var filename in queryMatchingFiles)
            Console.WriteLine(filename);

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    // Read the contents of the file.
    static string GetFileText(string name)
    {
        string fileContents = string.Empty;

        // If the file has been deleted since we took
        // the snapshot, ignore it and return the empty string.
        if (File.Exists(name))
            fileContents = File.ReadAllText(name);

        return fileContents;
    }
}
