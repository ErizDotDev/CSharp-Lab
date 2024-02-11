[assembly: CLSCompliant(true)]

public class Outer<T>
{
    T value;

    public Outer(T value)
    {
        this.value = value;
    }

    public class Inner1A : Outer<T>
    {
        public Inner1A(T value) : base(value) { }
    }

    public class Inner1B<U> : Outer<T>
    {
        U value2;

        public Inner1B(T value1, U value2) : base(value1)
        {
            this.value2 = value2;
        }
    }
}

class Program
{
    public static void Main()
    {
        var inst1 = new Outer<string>("This");
        Console.WriteLine(inst1);

        var inst2 = new Outer<string>.Inner1A("Another");
        Console.WriteLine(inst2);

        var inst3 = new Outer<string>.Inner1B<int>("That", 2);
        Console.WriteLine(inst2);
    }
}