namespace DC.Lab;

public class FindFileByExtension
{
    // This query will produce the full path for all .txt files
    // under the specified folder including subfolders.
    // It orders the list according to the file name.
    internal static void Execute()
    {
        string startFolder = @"C:\Program Files\dotnet";

        // Take a snapshot of the file system
        var dir = new DirectoryInfo(startFolder);

        // This method assumes that the application has discovery permissions
        // for all folders under the specified path.
        var fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);

        var fileQuery =
            from file in fileList
            where file.Extension == ".txt"
            orderby file.Name
            select file;

        // Execute the query. This might write out a lot of files!
        foreach (var fi in fileQuery)
            Console.WriteLine(fi.FullName);

        // Create and execute a new query by using the previous
        // query as a starting point. fileQuery is not
        // executed again until the call to Last()
        var newestFile =
            (from file in fileQuery
             orderby file.CreationTime
             select new { file.FullName, file.CreationTime }
            ).Last();

        Console.WriteLine($"\r\nThe newest .txt file is {newestFile.FullName}. Creation time: {newestFile.CreationTime}");

        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
    }
}
