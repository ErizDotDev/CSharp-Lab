namespace DC.Lab;

class Program
{
    interface IFruit
    {
        static abstract bool HasSeeds { get; }
    }

    record Watermelon : IFruit
    {
        public static bool HasSeeds => true;
    }

    record Grape : IFruit
    {
        public static bool HasSeeds => false;
    }

    static void DisplayHasSeeds<T>() where T : IFruit
    {
        Console.WriteLine(T.HasSeeds);
    }

    static void Main()
    {
        DisplayHasSeeds<Watermelon>();
        DisplayHasSeeds<Grape>();
    }
}