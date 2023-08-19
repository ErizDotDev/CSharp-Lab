namespace DC.Lab;

public class EnumerableExtensionDemo
{
    public static void Execute()
    {
        double[] numbers = { 1.9, 2, 8, 4, 5.7, 6, 7.2, 0 };
        var query = numbers.Median();

        Console.WriteLine($"double: Median = {query}");

        int[] numbers2 = { 1, 2, 3, 4, 5 };
        var query2 = numbers2.Median();

        Console.WriteLine($"int: Median = {query2}");

        int[] numbers3 = { 6, 7, 8, 9, 10 };

        /*
             You can use the num => num lambda expression as a parameter
             for the Median method so that the compiler can implicitly
             convert its value to double. If there is no implicit conversion
             the compiler will display an error message.
         */
        var query3 = numbers3.Median(num => num);

        Console.WriteLine($"int (using generic approach): Median = {query3}");

        string[] numbers4 = { "one", "two", "three", "four", "five" };
        var query4 = numbers4.Median(str => str.Length);

        Console.WriteLine($"string: Median = {query4}");

        string[] strings = { "a", "b", "c", "d", "e" };
        var query5 = strings.AlternateElements();

        Console.WriteLine();

        foreach (var element in query5)
            Console.WriteLine(element);
    }
}
