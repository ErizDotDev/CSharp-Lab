namespace DC.Lab;

public class QueryDuplicateFileNames
{
    // A Group key that can be passed to a separate method.
    // Override Equals and GetHashCode to define equality for the key.
    // Override ToString() to provide a user-friendly name Key.ToString()
    class PortableKey
    {
        public string Name { get; set; } = string.Empty;
        public DateTime LastWriteTime { get; set; }
        public long Length { get; set; }

        public override bool Equals(object? obj)
        {
            var other = (PortableKey?)obj;

            return other.LastWriteTime == this.LastWriteTime &&
                other.Length == this.Length &&
                other.Name == this.Name;
        }

        public override int GetHashCode()
        {
            var str = $"{this.LastWriteTime}{this.Length}{this.Name}";
            return str.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.Name} {this.Length} {this.LastWriteTime}";
        }
    }

    public static void Execute()
    {
        //QueryDuplicates();
        QueryDuplicates2();

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    private static void QueryDuplicates()
    {
        var startFolder = @"C:\Program Files\Microsoft Visual Studio\2022\Community\Common7";
        var dir = new DirectoryInfo(startFolder);
        var fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);

        int charsToSkip = startFolder.Length;

        var queryDuplicateNames =
            from file in fileList
            group file.FullName.Substring(charsToSkip) by file.Name into fileGroup
            where fileGroup.Count() > 1
            select fileGroup;

        PageOutput<string, string>(queryDuplicateNames);
    }

    private static void QueryDuplicates2()
    {
        var startFolder = @"C:\Program Files\Microsoft Visual Studio\2022\Community\Common7";
        var dir = new DirectoryInfo(startFolder);
        var fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);

        int charsToSkip = startFolder.Length;

        // Note the use of a compound key. Files that match
        // all three properties belong to the same group.
        // A named type is used to enable the query to be
        // passed to another method. Anonymous types can also be used
        // for composite keys but cannot be passed across method boundaries
        var queryDupFiles =
            from file in fileList
            group file.FullName.Substring(charsToSkip) by
                new PortableKey
                {
                    Name = file.Name,
                    LastWriteTime = file.LastWriteTime,
                    Length = file.Length
                } into fileGroup
            where fileGroup.Count() > 1
            select fileGroup;

        var list = queryDupFiles.ToList();

        int i = queryDupFiles.Count();

        PageOutput<PortableKey, string>(list);
    }

    // A generic method to page the output of the QueryDuplications methods
    // Here the type of the group must be specified explicitly. "var" cannot
    // be used in the method signatures. This method does not display more than one
    // group per page.
    private static void PageOutput<K, V>(IEnumerable<IGrouping<K, V>> groupByExtensionList)
    {
        // Flag to break out of paging loop.
        bool goAgain = true;

        // "3" = 1 line for extension + 1 for "Press any key" + 1 for input cursor.
        int numLines = Console.WindowHeight - 3;

        // Iterate through the outer collection of groups
        foreach (var fileGroup in groupByExtensionList)
        {
            // Start a new extension at the top of a page.
            int currentLine = 0;

            // Output only as many lines of the current group as will fit in the window.
            do
            {
                Console.Clear();
                Console.WriteLine($"Filename = {(fileGroup.Key.ToString() == String.Empty ? "[none]" : fileGroup.Key.ToString())}");

                // Get 'numLines' number of items starting at number 'currentLine'
                var resultPage = fileGroup.Skip(currentLine).Take(numLines);

                // Execute resultPageQuery
                foreach (var fileName in resultPage)
                    Console.WriteLine($"\t{fileName}");

                // Increment the line counter
                currentLine += numLines;

                // Give the user a chance to escape.
                Console.WriteLine("Press any key to continue or the 'End' key to break...");

                var key = Console.ReadKey().Key;
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
