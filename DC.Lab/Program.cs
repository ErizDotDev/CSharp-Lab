namespace DC.Lab;

class Program
{
    static void Main()
    {
        // EXAMPLE 1
        Console.WriteLine("Printing example 1...\n");

        var rnd = new Random();

        for (int ctr = 0; ctr <= 9; ctr++)
        {
            int randomN = rnd.Next(Int32.MinValue, Int32.MaxValue);
            var n = new Number(randomN);
            Console.WriteLine($"n = {n,12}, hash code = {n.GetHashCode(),12}");
        }

        // EXAMPLE 2
        Console.WriteLine("\nPrinting example 2...\n");

        var pt = new Point2(5, 8);
        Console.WriteLine(pt.GetHashCode());

        pt = new Point2(8, 5);
        Console.WriteLine(pt.GetHashCode());
    }
}