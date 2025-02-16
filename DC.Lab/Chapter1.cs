namespace DC.Lab;

public static class Chapter1
{
    public static void DoExercise2()
    {
        Console.WriteLine("Exercise 2");

        var negateIsEven = Negate(IsEven);
        Console.WriteLine(negateIsEven(1)); //expecting to be true
        Console.WriteLine(negateIsEven(2)); //expecting to be false
    }

    public static void DoExercise3()
    {
        Console.WriteLine("Exercise 3");

        var numbers = new[] { 5, 2, 9, 1, 5, 6 };
        var sortedNumbers = numbers.QuickSort();

        foreach (var el in sortedNumbers)
        {
            Console.Write($"{el} ");
        }
    }

    private static Func<int, bool> IsEven => (num) => num % 2 == 0;

    private static Func<TInput, bool> Negate<TInput>(Func<TInput, bool> func) => (input) => !func(input);
}

public static class EnumerableExtensions
{
    public static IEnumerable<T> QuickSort<T>(this IEnumerable<T> source) where T : IComparable<T>
    {
        if (source.Count() <= 1)
        {
            return source;
        }

        var pivot = source.First();
        var left = source.Skip(1).Where(x => x.CompareTo(pivot) < 0);
        var right = source.Skip(1).Where(x => x.CompareTo(pivot) >= 0);

        return QuickSort(left).Concat(new[] { pivot }).Concat(QuickSort(right));
    }
}
