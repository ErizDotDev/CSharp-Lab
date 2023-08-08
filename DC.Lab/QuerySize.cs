namespace DC.Lab;

public class QuerySize
{
    internal static void Execute()
    {
        string startFolder = @"C:\Program Files\Microsoft Visual Studio\2022\Community\VC#";
        var fileList = Directory.GetFiles(startFolder, "*.*", SearchOption.AllDirectories);

        var fileQuery = from file in fileList
                        select GetFileLength(file);

        // Cache the results to avoid multiple trips to the file system.
        long[] fileLengths = fileQuery.ToArray();

        // Return the size of the largest file
        long largestFile = fileLengths.Max();

        // Return the total number of bytes in all the files under the specified folder.
        long totalBytes = fileLengths.Sum();

        Console.WriteLine($"There are {totalBytes} bytes in {fileList.Count()} files under {startFolder}");
        Console.WriteLine($"The largest files is {largestFile} bytes.");

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    // This method is used to swallow the possible exception
    // that can be raised when accessing the FileInfo.Length property.
    static long GetFileLength(string fileName)
    {
        long retVal;

        try
        {
            var fi = new FileInfo(fileName);
            retVal = fi.Length;
        }
        catch (FileNotFoundException)
        {
            // If a file is no longer present,
            // just add zero bytes to the total.
            retVal = 0;
        }

        return retVal;
    }
}
