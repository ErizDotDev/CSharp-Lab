namespace DC.Lab;

public delegate void ThresholdReachedEventHandler(object sender, ThresholdReachedEventArgs e);

internal class Counter3
{
    private int threshold;
    private int total;

    public Counter3(int passedThreshold)
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

    public event ThresholdReachedEventHandler ThresholdReached;
}
