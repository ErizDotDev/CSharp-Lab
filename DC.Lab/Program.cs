namespace DC.Lab;

public class Program
{
    static void Main()
    {
        ExecuteFirstCodeSample();
    }

    static void ExecuteFirstCodeSample()
    {
        var auto = new Automobile("Lynx", 2016, 4, "V8");

        Console.WriteLine(auto.ToString());
        Console.WriteLine(auto.ToString("A"));
    }
}