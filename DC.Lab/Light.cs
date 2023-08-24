namespace DC.Lab;

public class OverheadLight : ILight, ITimerLight, IBlinkingLight
{
    private bool isOn;

    public bool IsOn() => isOn;

    public void SwitchOff() => isOn = false;

    public void SwitchOn() => isOn = true;

    public override string ToString()
        => $"The light is {(isOn ? "on" : "off")}";
}

public class HalogenLight : ITimerLight
{
    private enum HalogenLightState
    {
        Off,
        On,
        TimerModeOn
    }

    private HalogenLightState state;

    public void SwitchOn() => state = HalogenLightState.On;

    public void SwitchOff() => state = HalogenLightState.Off;

    public bool IsOn() => state != HalogenLightState.Off;

    public async Task TurnOnFor(int duration)
    {
        Console.WriteLine("Halogen light starting timer function.");
        state = HalogenLightState.TimerModeOn;
        await Task.Delay(duration);
        state = HalogenLightState.Off;
        Console.WriteLine("Halogen light finished custom timer function.");
    }

    public override string ToString()
        => $"The light is {state}";
}

public class LEDLight : IBlinkingLight, ITimerLight, ILight
{
    private bool isOn;

    public bool IsOn() => isOn;

    public void SwitchOff() => isOn = false;

    public void SwitchOn() => isOn = true;

    public async Task Blink(int duration, int repeatCount)
    {
        Console.WriteLine("LED Light starting the Blink function.");
        await Task.Delay(duration);
        Console.WriteLine("LED Light has finished the Blink function.");
    }

    public override string ToString()
        => $"The light is {(isOn ? "on" : "off")}";
}

public class ExtraFancyLight : IBlinkingLight, ITimerLight, ILight
{
    private bool isOn;

    public bool IsOn() => isOn;

    public void SwitchOff() => isOn = false;

    public void SwitchOn() => isOn = true;

    public async Task Blink(int duration, int repeatCount)
    {
        Console.WriteLine("Extra Fancy Light starting the Blink function.");
        await Task.Delay(duration);
        Console.WriteLine("Extra Fancy Light has finished the Blink function.");
    }

    public async Task TurnOn(int duration)
    {
        Console.WriteLine("Extra Fancy Light starting the Blink function.");
        await Task.Delay(duration);
        Console.WriteLine("Extra Fancy Light has finished the Blink function.");
    }

    public override string ToString()
        => $"The light is {(isOn ? "on" : "off")}";
}
