namespace DC.Lab;

public class Program
{
    static void Main()
    {
        //ExecuteFirstCodeSample();
        //ExecuteSecondCodeSample();
        ExecuteThirdCodeSample();
    }

    static void ExecuteFirstCodeSample()
    {
        var auto = new Automobile("Lynx", 2016, 4, "V8");

        Console.WriteLine(auto.ToString());
        Console.WriteLine(auto.ToString("A"));
    }

    static void ExecuteSecondCodeSample()
    {
        var list = new CList<int>();

        list.Add(1000);
        list.Add(2000);

        Console.WriteLine(list.ToString());
    }

    static void ExecuteThirdCodeSample()
    {
        var list = new List<int>();

        list.Add(1000);
        list.Add(2000);

        Console.WriteLine(list.ToString2());
        Console.WriteLine(list.ToString("N0"));
    }
}