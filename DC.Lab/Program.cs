[assembly: CLSCompliant(true)]

public class TemperatureChangedEventArgs : EventArgs
{
    private Decimal originalTemp;
    private Decimal newTemp;
    private DateTimeOffset when;

    public TemperatureChangedEventArgs(Decimal original, Decimal @new, DateTimeOffset time)
    {
        originalTemp = original;
        newTemp = @new;
        when = time;
    }

    public Decimal OldTemperature => originalTemp;

    public Decimal CurrentTemperature => newTemp;

    public DateTimeOffset Time => when;
}

public delegate void TemperatureChanged(object sender, TemperatureChangedEventArgs e);

public class Temperature
{
    private struct TemperatureInfo
    {
        public Decimal Temperature;
        public DateTimeOffset Recorded;
    }

    public event TemperatureChanged TemperatureChanged = null!;

    private Decimal previous;
    private Decimal current;
    private Decimal tolerance;
    private List<TemperatureInfo> tis = new List<TemperatureInfo>();

    public Temperature(Decimal temperature, Decimal tolerance)
    {
        current = temperature;

        var ti = new TemperatureInfo
        {
            Temperature = temperature,
        };
        tis.Add(ti);
        ti.Recorded = DateTimeOffset.UtcNow;

        this.tolerance = tolerance;
    }

    public Decimal CurrentTemperature
    {
        get => current;
        set
        {
            var ti = new TemperatureInfo
            {
                Temperature = value,
                Recorded = DateTimeOffset.UtcNow
            };

            previous = current;
            current = value;

            if (Math.Abs(current - previous) >= tolerance)
                raise_TemperatureChanged(new TemperatureChangedEventArgs(previous, current, ti.Recorded));
        }
    }

    public void raise_TemperatureChanged(TemperatureChangedEventArgs e)
    {
        if (TemperatureChanged is null)
            return;

        foreach (TemperatureChanged d in TemperatureChanged.GetInvocationList())
        {
            if (d.Method.Name.Contains("Duplicate"))
                Console.WriteLine("Duplicate event handler; event handler not executed.");
            else
                d.Invoke(this, e);
        }
    }
}

class Program
{
    public Temperature temp;

    public static void Main()
    {
        Program p = new Program();
    }

    public Program()
    {
        temp = new Temperature(65, 3);
        temp.TemperatureChanged += this.TemperatureNotification;
        RecordTemperatures();

        Program p = new Program(temp);
        p.RecordTemperatures();
    }

    public Program(Temperature t)
    {
        temp = t;
        RecordTemperatures();
    }

    public void RecordTemperatures()
    {
        temp.TemperatureChanged += this.DuplicateTemperatureNotification;
        temp.CurrentTemperature = 66;
        temp.CurrentTemperature = 63;
    }

    internal void TemperatureNotification(Object sender, TemperatureChangedEventArgs e)
    {
        Console.WriteLine($"Notification 1: The temperature changed from {e.OldTemperature} to {e.CurrentTemperature}");
    }

    public void DuplicateTemperatureNotification(Object sender, TemperatureChangedEventArgs e)
    {
        Console.WriteLine($"Notification 2: The temperature changed from {e.OldTemperature} to {e.CurrentTemperature}");
    }
}