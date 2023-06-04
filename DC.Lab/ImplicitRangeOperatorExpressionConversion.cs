namespace DC.Lab;

internal class ImplicitRangeOperatorExpressionConversion
{
    public static void Execute()
    {
        Range implicitRange = 3..^5;

        Range explicitRange = new(
            start: new Index(value: 3, fromEnd: false),
            end: new Index(value: 5, fromEnd: true));

        if (implicitRange.Equals(explicitRange))
        {
            Console.WriteLine($"The implicit range '{implicitRange}' equals the explicit range '{explicitRange}'");
        }
    }
}
