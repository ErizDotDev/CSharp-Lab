using System.Diagnostics;

namespace DC.Lab;

class Program
{
    static async Task Main(string[] args)
    {
        var watch = new Stopwatch();

        watch.Start();

        Coffee cup = PourCoffee();
        Console.WriteLine("coffee is ready");

        Juice oj = PourOJ();
        Console.WriteLine("oj is ready");

        Task<Egg> eggTask = FryEggsAsync(2);
        Task<Bacon> baconTask = FryBaconAsync(3);
        Task<Toast> toastTask = MakeToastWithButterAndJamAsync(2);

        var breakfastTasks = new List<Task> { eggTask, baconTask, toastTask };

        while (breakfastTasks.Count > 0)
        {
            Task finishedTask = await Task.WhenAny(breakfastTasks);

            if (finishedTask == eggTask)
                Console.WriteLine("eggs are ready");
            else if (finishedTask == baconTask)
                Console.WriteLine("bacon is ready");
            else if (finishedTask == toastTask)
                Console.WriteLine("toast is ready");

            await finishedTask;
            breakfastTasks.Remove(finishedTask);
        }

        Console.WriteLine("Breakfast is ready!");

        watch.Stop();

        Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
    }

    private static Coffee PourCoffee()
    {
        Console.WriteLine("pouring coffee");
        return new Coffee();
    }

    private static async Task<Egg> FryEggsAsync(int howMany)
    {
        Console.WriteLine("warming the egg pan...");
        Task.Delay(3000).Wait();
        Console.WriteLine($"cracking {howMany} eggs");
        Console.WriteLine("cooking the eggs...");
        Task.Delay(3000).Wait();
        Console.WriteLine("Put eggs on plate");

        return new Egg();
    }

    private static async Task<Bacon> FryBaconAsync(int slices)
    {
        Console.WriteLine($"putting {slices} slices of bacon in the pan");
        Console.WriteLine("cooking first side of bacon...");
        Task.Delay(3000).Wait();

        for (int slice = 0; slice < slices; slice++)
        {
            Console.WriteLine("flipping a side of bacon");
        }

        Console.WriteLine("cooking the second side of bacon...");
        Task.Delay(3000).Wait();
        Console.WriteLine("put bacon on plate");

        return new Bacon();
    }

    private static async Task<Toast> MakeToastWithButterAndJamAsync(int number)
    {
        var toast = await ToastBreadAsync(number);
        ApplyButter(toast);
        ApplyJam(toast);

        return toast;
    }

    private static async Task<Toast> ToastBreadAsync(int slices)
    {
        for (int slice = 0; slice < slices; slice++)
        {
            Console.WriteLine("Putting a slice of bread in the toaster");
        }

        Console.WriteLine("Start toasting...");
        await Task.Delay(2000);

        //Console.WriteLine("Fire! Toast is ruined!");
        //throw new InvalidOperationException("The toaster is on fire");

        await Task.Delay(1000);
        Console.WriteLine("Remove toast from toaster");

        return new Toast();
    }

    private static void ApplyButter(Toast toast) =>
        Console.WriteLine("Putting butter on the toast");

    private static void ApplyJam(Toast toast) =>
        Console.WriteLine("Putting jam on the toast");

    private static Juice PourOJ()
    {
        Console.WriteLine("Pouring orange juice");
        return new Juice();
    }
}

internal class Bacon { }

internal class Coffee { }

internal class Toast { }

internal class Egg { }

internal class Juice { }
