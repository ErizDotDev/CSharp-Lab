namespace DC.Lab;

internal class IndicesRangesScenarios
{
    public static void Execute()
    {
        int[] sequence = Sequence(100);

        for (int start = 0; start < sequence.Length; start += 100)
        {
            Range r = start..(start + 10);
            var (min, max, average) = MovingAverage(sequence, r);
            Console.WriteLine($"From {r.Start} to {r.End}:\tMin: {min},\tMax: {max},\tAverage: {average}");
        }

        for (int start = 0; start < sequence.Length; start += 100)
        {
            Range r = ^(start + 10)..^start;
            var (min, max, average) = MovingAverage(sequence, r);
            Console.WriteLine($"From {r.Start} to {r.End}\tMin: {min},\tMax {max},\tAverage: {average}");
        }
    }

    private static int[] Sequence(int count) =>
        Enumerable.Range(0, count).Select(x => (int)(Math.Sqrt(x) * 100)).ToArray();

    private static (int min, int max, double average) MovingAverage(int[] subSequence, Range range) =>
        (
            subSequence[range].Min(),
            subSequence[range].Max(),
            subSequence[range].Average()
        );
}
