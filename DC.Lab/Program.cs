using System.Collections.Concurrent;

namespace DC.Lab;

class Program
{
    static void Main()
    {
        var buffer = new BlockingCollection<int>(boundedCapacity: 5);

        Task.Run(() => Producer(buffer));
        Task.Run(() => Consumer(buffer));

        Console.ReadLine();
    }

    static void Producer(BlockingCollection<int> buffer)
    {
        for (int i = 0; i < 10; i++)
        {
            buffer.Add(i);
            Console.WriteLine($"Produced: {i}");
            Thread.Sleep(100);
        }

        buffer.CompleteAdding();
    }

    static void Consumer(BlockingCollection<int> buffer)
    {
        foreach (var item in buffer.GetConsumingEnumerable())
        {
            Console.WriteLine($"Consumed: {item}");
            Thread.Sleep(200);
        }
    }
}