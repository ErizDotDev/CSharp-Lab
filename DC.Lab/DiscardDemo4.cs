namespace DC.Lab;

internal class DiscardDemo4
{
    public static async Task Execute()
    {
        Console.WriteLine("About to launch a task.");

        _ = Task.Run(() =>
        {
            var iterations = 0;

            for (int ctr = 0; ctr < int.MaxValue; ctr++)
                iterations++;

            Console.WriteLine("Completed looping operation...");

            throw new InvalidOperationException();
        });

        await Task.Delay(5000);

        Console.WriteLine("Exiting after 5 seconds display.");
    }
}
