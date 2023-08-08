namespace DC.Lab;

public class FileComparer
{
    public static void Execute()
    {
        string pathA = @"C:\Users\User\Documents\.dc\Test\Folder 1";
        string pathB = @"C:\Users\User\Documents\.dc\Test\Folder 2";

        var dir1 = new DirectoryInfo(pathA);
        var dir2 = new DirectoryInfo(pathB);

        var list1 = dir1.GetFiles("*.*", SearchOption.AllDirectories);
        var list2 = dir2.GetFiles("*.*", SearchOption.AllDirectories);

        var myFileCompare = new FileCompare();

        // This query determines whether the two folders contain
        // identical file lists, based on the custom file comparer
        // that is defined in the FileCompare class.
        // The query executes immediately because it returns a bool.
        bool areIdentical = list1.SequenceEqual(list2, myFileCompare);

        if (areIdentical)
            Console.WriteLine("The two folders are the same.");
        else
            Console.WriteLine("The two folders are not the same.");

        // Find the common files. It produces a sequence and doesn't
        // execute until the foreach statement.
        var queryCommandFiles = list1.Intersect(list2, myFileCompare);

        if (queryCommandFiles.Any())
        {
            Console.WriteLine("The following files are in both folders:");

            foreach (var v in queryCommandFiles)
            {
                Console.WriteLine(v.FullName);
            }
        }
        else
        {
            Console.WriteLine("There are no common files in the two folders.");
        }

        // Find the set difference between the two folders.
        // For this example we only check one way.
        var queryList1Only = (from file in list1
                              select file).Except(list2, myFileCompare);

        Console.WriteLine("The following files are in list1 but not in list2");

        foreach (var v in queryList1Only)
        {
            Console.WriteLine(v.FullName);
        }

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}

// This implementation defines a very simple comparison
// between two FileInfo objects. It only compares the name
// of the files being compared and their length in bytes.
public class FileCompare : IEqualityComparer<FileInfo>
{
    public FileCompare() { }

    public bool Equals(FileInfo? f1, FileInfo? f2)
    {
        return (f1?.Name == f2?.Name &&
            f1?.Length == f2?.Length);
    }

    // Return a hash that reflects the comparison criteria. According to the 
    // rules for IEqualityComparer<T>, if Equals is true, then the hash codes must
    // also be equal. Because equality as defined here is a simple value equality, not
    // reference identity, it is possible that two or more objects will produce the same
    // hash code.
    public int GetHashCode(FileInfo fi)
    {
        var s = $"{fi.Name}{fi.Length}";
        return s.GetHashCode();
    }
}
