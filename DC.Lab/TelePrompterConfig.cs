using static System.Math;

namespace DC.Lab;

internal class TelePrompterConfig
{
    public int DelayInMilliseconds { get; private set; } = 200;

    public bool Done { get; private set; }

    public void UpdateDelay(int increment)
    {
        var newDelay = Min(DelayInMilliseconds + increment, 100);
        newDelay = Max(newDelay, 20);
        DelayInMilliseconds = newDelay;
    }

    public void SetDone()
    {
        Done = true;
    }
}
