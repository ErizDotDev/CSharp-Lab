namespace DC.Lab;

internal class IndicesRangesTypeSupport
{
    public static void Execute()
    {
        var jagged = new int[10][]
        {
            new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
            new int[10] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 },
            new int[10] { 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 },
            new int[10] { 30, 31, 32, 33, 34, 35, 36, 37, 38, 39 },
            new int[10] { 40, 41, 42, 43, 44, 45, 46, 47, 48, 49 },
            new int[10] { 50, 51, 52, 53, 54, 55, 56, 57, 58, 59 },
            new int[10] { 60, 61, 62, 63, 64, 65, 66, 67, 68, 69 },
            new int[10] { 70, 71, 72, 73, 74, 75, 76, 77, 78, 79 },
            new int[10] { 80, 81, 82, 83, 84, 85, 86, 87, 88, 89 },
            new int[10] { 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 },
        };

        var selectedRows = jagged[3..^3];

        foreach (var row in selectedRows)
        {
            var selectedColumns = row[2..^2];
            foreach (var cell in selectedColumns)
            {
                Console.Write($"{cell}, ");
            }

            Console.WriteLine();
        }
    }
}
