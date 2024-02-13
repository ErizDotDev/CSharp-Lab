namespace DC.Lab;

class Program
{
    public static void Main()
    {
        TemperatureCelsius tempC1 = new(0);
        TemperatureFahrenheit tempF1 = (TemperatureFahrenheit)Convert.ChangeType(tempC1,
            typeof(TemperatureFahrenheit), null);

        Console.WriteLine($"{tempC1} equals {tempF1}");

        TemperatureCelsius tempC2 = (TemperatureCelsius)Convert.ChangeType(tempC1,
            typeof(TemperatureCelsius), null);

        Console.WriteLine($"{tempC1} equals {tempC2}");

        TemperatureFahrenheit tempF2 = new(212);
        TemperatureCelsius tempC3 = (TemperatureCelsius)Convert.ChangeType(tempF2,
            typeof(TemperatureCelsius), null);

        Console.WriteLine($"{tempF2} equals {tempC3}");

        TemperatureFahrenheit tempF3 = (TemperatureFahrenheit)Convert.ChangeType(tempF2,
            typeof(TemperatureFahrenheit), null);

        Console.WriteLine($"{tempF2} equals {tempF3}");
    }
}