using System.Globalization;

namespace DC.Lab;

internal static class DiscardDemo3
{
    public static void Execute()
    {
        object?[] objects =
        {
            CultureInfo.CurrentCulture,
            CultureInfo.CurrentCulture.DateTimeFormat,
            CultureInfo.CurrentCulture.NumberFormat,
            new ArgumentException(),
            null
        };

        foreach (var obj in objects)
            ProvidesFormatInfo(obj);
    }

    private static void ProvidesFormatInfo(object? obj)
    {
        Console.WriteLine(obj switch
        {
            IFormatProvider fmt => $"{fmt.GetType()} object",
            null => "A null object reference: Its use could result in a NullReferenceException",
            _ => "Some object type without format information"
        }
        );
    }
}
