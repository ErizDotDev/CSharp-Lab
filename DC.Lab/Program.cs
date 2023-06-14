using System.Diagnostics;

namespace DC.Lab;

class Program
{
    static readonly HttpClient s_client = new HttpClient
    {
        MaxResponseContentBufferSize = 1_000_000
    };

    static readonly IEnumerable<string> s_urlList = new string[]
    {
        "https://learn.microsoft.com",
        "https://learn.microsoft.com/aspnet/core",
        "https://learn.microsoft.com/azure",
        "https://learn.microsoft.com/azure/devops",
        "https://learn.microsoft.com/dotnet",
        "https://learn.microsoft.com/dynamics365",
        "https://learn.microsoft.com/education",
        "https://learn.microsoft.com/enterprise-mobility-security",
        "https://learn.microsoft.com/gaming",
        "https://learn.microsoft.com/graph",
        "https://learn.microsoft.com/microsoft-365",
        "https://learn.microsoft.com/office",
        "https://learn.microsoft.com/powershell",
        "https://learn.microsoft.com/sql",
        "https://learn.microsoft.com/surface",
        "https://learn.microsoft.com/system-center",
        "https://learn.microsoft.com/visualstudio",
        "https://learn.microsoft.com/windows",
        "https://learn.microsoft.com/xamarin"
    };

    //static Task Main(string[] args) => SumPageSizesAsync();
    static Task Main(string[] args) => DemoProcessTasksAsTheyComplete();

    static async Task SumPageSizesAsync()
    {
        var stopwatch = Stopwatch.StartNew();

        IEnumerable<Task<int>> downloadTasksQuery =
            from url in s_urlList
            select ProcessUrlAsync(url, s_client);

        List<Task<int>> downloadTasks = downloadTasksQuery.ToList();

        int total = 0;
        while (downloadTasks.Any())
        {
            Task<int> finishedTask = await Task.WhenAny(downloadTasks);
            downloadTasks.Remove(finishedTask);
            total += await finishedTask;
        }

        stopwatch.Stop();

        Console.WriteLine($"\nTotal bytes returned: {total:#,#}");
        Console.WriteLine($"Elapsed time:\t\t{stopwatch.Elapsed}");
    }

    static async Task<int> ProcessUrlAsync(string url, HttpClient client)
    {
        byte[] content = await client.GetByteArrayAsync(url);
        Console.WriteLine($"{url,-60} {content.Length,10:#,#}");

        return content.Length;
    }

    static async Task DemoProcessTasksAsTheyComplete()
    {
        var tasks = new[]
        {
            Task.Delay(3000).ContinueWith(_ => 3),
            Task.Delay(1000).ContinueWith(_ => 1),
            Task.Delay(2000).ContinueWith(_ => 2),
            Task.Delay(5000).ContinueWith(_ => 5),
            Task.Delay(4000).ContinueWith(_ => 4),
        };

        //await ProcessInterleavedTasks(tasks);
        await ProcessNonInterleavedTasks(tasks);
    }

    static async Task ProcessInterleavedTasks<T>(IEnumerable<Task<T>> tasks)
    {
        foreach (var bucket in Interleaved(tasks))
        {
            var t = await bucket;
            var result = await t;
            Console.WriteLine($"{DateTime.Now}: {result}");
        }
    }

    static async Task ProcessNonInterleavedTasks<T>(IEnumerable<Task<T>> tasks)
    {
        foreach (var bucket in tasks)
        {
            var t = await bucket;
            Console.WriteLine($"{DateTime.Now}: {t}");
        }
    }

    static Task<Task<T>>[] Interleaved<T>(IEnumerable<Task<T>> tasks)
    {
        var inputTasks = tasks.ToList();

        var buckets = new TaskCompletionSource<Task<T>>[inputTasks.Count];
        var results = new Task<Task<T>>[buckets.Length];

        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i] = new TaskCompletionSource<Task<T>>();
            results[i] = buckets[i].Task;
        }

        int nextTaskIndex = -1;
        Action<Task<T>> continuation = completed =>
        {
            var bucket = buckets[Interlocked.Increment(ref nextTaskIndex)];
            bucket.TrySetResult(completed);
        };

        foreach (var inputTask in inputTasks)
        {
            inputTask.ContinueWith(continuation, CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
        }

        return results;
    }
}