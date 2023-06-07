using DC.Lab;

await RunTeleprompter();

static async Task RunTeleprompter()
{
    var config = new TelePrompterConfig();
    var displayTask = ShowTeletrompter(config);

    var speedTask = GetInput(config);
    await Task.WhenAny(displayTask, speedTask);
}

static async Task ShowTeletrompter(TelePrompterConfig config)
{
    var words = ReadFrom("SampleQuotes.txt");

    foreach (var word in words)
    {
        Console.WriteLine(word);

        if (!string.IsNullOrWhiteSpace(word))
            await Task.Delay(config.DelayInMilliseconds);
    }

    config.SetDone();
}

static IEnumerable<string> ReadFrom(string file)
{
    string? line;

    using (var reader = File.OpenText(file))
    {
        while ((line = reader.ReadLine()) != null)
        {
            var words = line.Split(' ');
            var lineLength = 0;

            foreach (var word in words)
            {
                yield return word + " ";

                lineLength += word.Length + 1;

                if (lineLength > 70)
                {
                    yield return Environment.NewLine;
                    lineLength = 0;
                }
            }

            yield return Environment.NewLine;
        }
    }
}

static async Task GetInput(TelePrompterConfig config)
{
    var delay = 200;

    Action work = () =>
    {
        do
        {
            var key = Console.ReadKey(true);

            if (key.KeyChar == '>')
                config.UpdateDelay(10);
            else if (key.KeyChar == '<')
                config.UpdateDelay(10);
            else if (key.KeyChar == 'X' || key.KeyChar == 'x')
                config.SetDone();
        }
        while (!config.Done);
    };

    await Task.Run(work);
}