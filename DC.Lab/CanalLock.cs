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
        HighWaterGateOpen = open;
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
