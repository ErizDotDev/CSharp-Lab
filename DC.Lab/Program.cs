namespace DC.Lab;

internal class Part : IEquatable<Part>, IComparable<Part>
{
    public string Name { get; set; } = null!;
    public int Id { get; set; }

    public override string ToString() => $"ID: {Id}\tName: {Name}";

    public override bool Equals(object? obj) =>
        (obj is Part part)
            ? Equals(part)
            : false;

    public int SortByNameAscending(string name1, string name2) =>
        name1?.CompareTo(name2) ?? 1;

    // Default comparer for Part type.
    // A null value means that this object is greater.
    public int CompareTo(Part? comparePart) =>
        comparePart is null
            ? 1
            : Id.CompareTo(comparePart.Id);

    public override int GetHashCode() => Id;

    public bool Equals(Part? other) =>
        other is null
            ? false
            : Id.Equals(other.Id);
}

class Program
{
    static void Main()
    {
        var parts = new List<Part>
        {
            new Part { Name = "regular seat", Id = 1434 },
            new Part { Name = "crank arm", Id = 1234 },
            new Part { Name = "shift lever", Id = 1634 },
            // Name intentionally left null.
            new Part { Id = 1334 },
            new Part { Name = "banana seat", Id = 1444 },
            new Part { Name = "cassette", Id = 1534 }
        };

        Console.WriteLine("Before sort");
        parts.ForEach(Console.WriteLine);

        parts.Sort();

        Console.WriteLine("\nAfter sort by part number.");
        parts.ForEach(Console.WriteLine);

        // The delegate approach.
        parts.Sort((Part x, Part y) =>
            x.Name is null && y.Name is null
                ? 0
                : x.Name is null
                    ? -1
                    : y.Name is null
                        ? 1
                        : x.Name.CompareTo(y.Name));

        Console.WriteLine("\nAfter sort by name.");
        parts.ForEach(Console.WriteLine);
    }
}