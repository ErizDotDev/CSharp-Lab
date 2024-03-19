namespace DC.Lab;

internal class Counter1
{
    private int threshold;
    private int total;

    public Counter1(int passedThreshold)
    {
        threshold = passedThreshold;
        ThresholdReached = default!;
    }

    public void Add(int x)
    {
        total += x;
        if (total >= threshold)
            ThresholdReached?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler ThresholdReached;
}
