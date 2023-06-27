using System.Collections.Concurrent;

namespace DC.Lab;

class Program
{
    static void Main(string[] args)
    {
        var number = new AsyncLocal<int>();

        number.Value = 42;
        MyThreadPool.QueueUserWorkItem(() => Console.WriteLine(number.Value));
        number.Value = 0;

        Console.ReadLine();
    }
}

class MyThreadPool
{
    private static readonly BlockingCollection<(Action, ExecutionContext?)> s_workItems = new();

    public static void QueueUserWorkItem(Action workItem)
    {
        s_workItems.Add((workItem, ExecutionContext.Capture()));
    }

    static MyThreadPool()
    {
        for (int i = 0; i < Environment.ProcessorCount; i++)
        {
            new Thread(() =>
            {
                while (true)
                {
                    (Action action, ExecutionContext? ec) = s_workItems.Take();

                    if (ec is null)
                        action();
                    else
                        ExecutionContext.Run(ec, s => ((Action)s!)(), action);
                }
            })
            { IsBackground = true }.UnsafeStart();
        }
    }
}