namespace DC.Lab;

public class QueryAString
{
    public static void Execute()
    {
        string randomString = "ABCDE99F-J74-12-89A";

        var query = from ch in randomString
                    where Char.IsDigit(ch)
                    select ch;

        foreach (var ch in query)
            Console.Write($"{ch} ");

        int count = query.Count();
        Console.WriteLine($"Count = {count}");

        // Select all characters before the first '-'
        var query2 = randomString.TakeWhile(c => c != '-');

        foreach (var c in query2)
            Console.Write(c);

        Console.WriteLine(Environment.NewLine + "Press any key to exit");
        Console.ReadKey();
    }
}
