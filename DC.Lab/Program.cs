namespace DC.Lab;

class Program
{
    static void Main()
    {
        var c = new Counter1(new Random().Next(10));

        c.ThresholdReached += c_ThresholdReached!;

        Console.WriteLine("Press 'a' key to increase total");

        while (Console.ReadKey(true).KeyChar == 'a')
        {
            Console.WriteLine("adding one");
            c.Add(1);
        }
    }

    static void c_ThresholdReached(object sender, EventArgs e)
    {
        Console.WriteLine("The threshold was reached.");
        Environment.Exit(0);
    }
}