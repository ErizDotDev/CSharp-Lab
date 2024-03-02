namespace DC.Lab;

public class Automobile
{
    public int Doors { get; init; }
    public string Cylinders { get; init; } = null!;
    public int Year { get; init; }
    public string Model { get; init; }

    public Automobile(string model, int year, int doors, string cylinders)
    {
        Model = model;
        Year = year;
        Doors = doors;
        Cylinders = cylinders;
    }

    public override string ToString() => ToString("G");

    public string ToString(string format)
    {
        if (string.IsNullOrEmpty(format))
            format = "G";

        var result = format.ToUpperInvariant() switch
        {
            "G" => $"{Year} {Model}",
            "D" => $"{Year} {Model}, {Doors} dr.",
            "C" => $"{Year} {Model}, {Cylinders}",
            "A" => $"{Year} {Model}, {Doors} dr. {Cylinders}",
            _ => throw new ArgumentException(
                $"'{format}' is an invalid format string.")
        };

        return result;
    }
}
