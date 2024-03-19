namespace DC.Lab;

class Program
{
    static void Main()
    {
        //var c = new Counter1(new Random().Next(10));
        //c.ThresholdReached += c_ThresholdReached!;

        //var c = new Counter2(new Random().Next(10));
        //c.ThresholdReached += c_ThresholdReached2!;

        var c = new Counter3(new Random().Next(10));
        c.ThresholdReached += c_ThresholdReached2;

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

    static void c_ThresholdReached2(object sender, ThresholdReachedEventArgs e)
    {
        Console.WriteLine($"The threshold of {e.Threshold} was reached at {e.TimeReached}");
        Environment.Exit(0);
    }
}