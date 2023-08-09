namespace DC.Lab;

public class QueryFileBySize
{
    public static void Execute()
    {
        var startFolder = @"C:\Program Files\Microsoft Visual Studio\2022\Community\Common7";

        var dir = new DirectoryInfo(startFolder);
        var fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);

        // Return the size of the largest file
        long maxSize = (from file in fileList
                        let len = GetFileLength(file)
                        where len > 0
                        orderby len descending
                        select len)
                        .First();

        Console.WriteLine($"The length of the largest file under {startFolder} is {maxSize}");

        // Return the FileInfo object for the largest file
        // by sorting and selecting from beginning of list
        var longestFile = (from file in fileList
                           let len = GetFileLength(file)
                           where len > 0
                           orderby len descending
                           select file)
                           .First();

        Console.WriteLine($"The largest file under {startFolder} is {longestFile.FullName} with {longestFile.Length} bytes");

        // Return the FileInfo of the smallest file.
        var smallestFile = (from file in fileList
                            let len = GetFileLength(file)
                            where len > 0
                            orderby len ascending
                            select file)
                            .First();

        Console.WriteLine($"The smallest file under {startFolder} is {smallestFile.FullName} with {smallestFile.Length} bytes");

        // Return the FileInfo for the 10 largest files
        // queryTenLargest is an IEnumerable<FileInfo>
        var queryTenLargest = (from file in fileList
                               let len = GetFileLength(file)
                               orderby len descending
                               select file)
                               .Take(10);

        Console.WriteLine($"The 10 largest files under {startFolder} are:");

        foreach (var v in queryTenLargest)
            Console.WriteLine($"{v.FullName}: {v.Length} bytes");

        // Group the files according to their size, leaving out
        // files that are less than 200000 bytes.
        var querySizeGroups = from file in fileList
                              let len = GetFileLength(file)
                              where len > 0
                              group file by (len / 10000) into fileGroup
                              where fileGroup.Key >= 2
                              orderby fileGroup.Key descending
                              select fileGroup;

        foreach (var fileGroup in querySizeGroups)
        {
            Console.WriteLine(fileGroup.Key.ToString() + "00000");

            foreach (var item in fileGroup)
            {
                Console.WriteLine($"\t{item.Name}: {item.Length}");
            }
        }

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    public static long GetFileLength(FileInfo fi)
    {
        long retVal;

        try
        {
            retVal = fi.Length;
        }
        catch (FileNotFoundException)
        {
            retVal = 0;
        }

        return retVal;
    }
}
