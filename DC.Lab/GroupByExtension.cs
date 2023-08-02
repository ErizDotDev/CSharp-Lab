namespace DC.Lab;

public class GroupByExtension
{
    // This query will sort all the files under the specified folder
    // and subfolder into groups keyed by the file extension.
    internal static void Execute()
    {
        // Take a snapshot of the file system.
        var startFolder = @"C:\Program Files\Microsoft Visual Studio\2022\Community\Common7";

        // Used in WriteLine to trim output lines.
        int trimLength = startFolder.Length;

        // Take a snapshot of the file system.
        var dir = new DirectoryInfo(startFolder);

        // This method assumes that the application has discovery permissions
        // for all folders under the specified path.
        var fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);

        // Create the query
        var queryGroupByExt =
            from file in fileList
            group file by file.Extension.ToLower() into fileGroup
            orderby fileGroup.Key
            select fileGroup;

        // Display one group at a time. If the number of
        // entries is greater than the number of lines
        // in the console window, then page the output.
        PageOutput(trimLength, queryGroupByExt);
    }

    // This method specifically handles group queries of FileInfo objects with string keys.
    // It can be modified to work for any long listings of data. Note that explicit typing
    // must be used in method signatures. The groupByExtList parameter is a query that produces
    // groups of FileInfo objects with string keys.
    private static void PageOutput(int rootLength,
        IEnumerable<IGrouping<string, FileInfo>> groupByExtList)
    {
        // Flag to break out of paging loop
        bool goAgain = true;

        // "3" = 1 line for extension + 1 for "Press any key" + "1 for input cursor.
        int numLines = Console.WindowHeight - 3;

        // Iterate through the outer collection of groups.
        foreach (var fileGroup in groupByExtList)
        {
            // Start a new extension at the top of a page.
            int currentLine = 0;

            // Output as many lines of the current group as will fit in the window.
            do
            {
                Console.Clear();
                Console.WriteLine(fileGroup.Key == string.Empty ? "[none]" : fileGroup.Key);

                // Get 'numLines' number of items starting at number 'currentLine'
                var resultPage = fileGroup.Skip(currentLine).Take(numLines);

                // Execute the resultPage query
                foreach (var f in resultPage)
                    Console.WriteLine($"\t{f.FullName.Substring(rootLength)}");

                currentLine += numLines;

                // Give the user a chance to escape.
                Console.WriteLine("Press any key to continue or the 'End' key to break...");
                ConsoleKey key = Console.ReadKey().Key;

                if (key == ConsoleKey.End)
                {
                    goAgain = false;
                    break;
                }
            } while (currentLine < fileGroup.Count());

            if (!goAgain)
                break;
        }
    }
}
