namespace DC.Lab;

public class Counter2
{
    private int threshold;
    private int total;

    public Counter2(int passedThreshold)
    {
        threshold = passedThreshold;
        ThresholdReached = default!;
    }

    public void Add(int x)
    {
        total += x;

        if (total >= threshold)
        {
            var args = new ThresholdReachedEventArgs();
            args.Threshold = threshold;
            args.TimeReached = DateTime.Now;
            OnThresholdReached(args);
        }
    }

    protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
    {
        var handler = ThresholdReached;
        if (handler is not null)
            handler(this, e);
    }

    public event EventHandler<ThresholdReachedEventArgs> ThresholdReached;
}

public class ThresholdReachedEventArgs : EventArgs
{
    public int Threshold { get; set; }
    public DateTime TimeReached { get; set; }
}
