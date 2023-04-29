namespace DC.Lab;

public enum WaterLevel
{
    Low,
    High
}

public class CanalLock
{
    // Query canal lock state:
    public WaterLevel CanalLockWaterLevel { get; private set; } = WaterLevel.Low;
    public bool HighWaterGateOpen { get; private set; } = false;
    public bool LowWaterGateOpen { get; private set; } = false;

    // Change the upper gate.
    public void SetHighGate(bool open)
    {
        HighWaterGateOpen = (open, HighWaterGateOpen, CanalLockWaterLevel) switch
        {
            (false, false, WaterLevel.High) => false,
            (false, false, WaterLevel.Low) => false,
            (false, true, WaterLevel.High) => false,
            (false, true, WaterLevel.Low) => false, // should never happen
            (true, false, WaterLevel.High) => true,
            (true, false, WaterLevel.Low) => throw new InvalidOperationException("Cannot open high gate when the water is low"),
            (true, true, WaterLevel.High) => true,
            (true, true, WaterLevel.Low) => false, // should never happen
            _ => throw new InvalidOperationException("Invalid internal state"),
        };
    }

    // Change the low gate.
    public void SetLowGate(bool open)
    {
        LowWaterGateOpen = open;
    }

    // Change water level.
    public void SetWaterLevel(WaterLevel newLevel)
    {
        CanalLockWaterLevel = newLevel;
    }

    public override string ToString() =>
        $"The lower gate is {(LowWaterGateOpen ? "Open" : "Closed")}. " +
        $"The upper gate is {(HighWaterGateOpen ? "Open" : "Closed")}. " +
        $"The water level is {CanalLockWaterLevel}";
}
