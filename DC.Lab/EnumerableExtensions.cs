namespace DC.Lab;

public static class EnumerableExtensions
{
    public static double Median(this IEnumerable<double>? source)
    {
        if (source is null || !source.Any())
            throw new InvalidOperationException("Cannot compute median for a null or empty set.");

        var sortedList =
            source.OrderBy(number => number).ToList();

        int itemIndex = sortedList.Count / 2;

        if (sortedList.Count % 2 == 0)
            return (sortedList[itemIndex] + sortedList[itemIndex - 1]) / 2;
        else
            return sortedList[itemIndex];
    }

    public static double Median(this IEnumerable<int> source) =>
        (from number in source select (double)number).Median();

    public static double Median<T>(
        this IEnumerable<T> numbers, Func<T, double> selector
    ) => (from num in numbers select selector(num)).Median();

    public static IEnumerable<T> AlternateElements<T>(this IEnumerable<T> source)
    {
        int index = 0;

        foreach (var element in source)
        {
            if (index % 2 == 0)
                yield return element;

            index++;
        }
    }
}
