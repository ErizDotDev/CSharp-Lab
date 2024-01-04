using System.Text;

class Program
{
    public static void OpEqualsTest<T>(T s, T t) where T : class
    {
        Console.WriteLine(s == t);
    }

    public static void Main(string[] args)
    {
        string s1 = "target";
        StringBuilder sb = new("target");
        string s2 = sb.ToString();
        OpEqualsTest<string>(s1, s2);
    }
}