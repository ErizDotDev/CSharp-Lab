using System.Reflection;

namespace DC.Lab;

public delegate void D1(C c, string s);
public delegate void D2(string s);
public delegate void D3();

public class C
{
    private int id;

    public C(int id)
    {
        this.id = id;
    }

    public void M1(string s)
    {
        Console.WriteLine($"Instance method M1 on C: id = {this.id}, s = {s}");
    }


    public static void M2(string s)
    {
        Console.WriteLine($"Static method M2 on C: s = {s}");
    }
}

class Program
{
    static void Main()
    {
        var c1 = new C(42);

        var mi1 = typeof(C).GetMethod("M1",
            BindingFlags.Public | BindingFlags.Instance);
        var mi2 = typeof(C).GetMethod("M2",
            BindingFlags.Public | BindingFlags.Static);

        D1 d1;
        D2 d2;
        D3 d3;

        Console.WriteLine("\nAn instance method closed over C.");

        //This scenario illustrates that the delegate and the method share the
        //same list of argument types.
        //Using delegate type D2 with instance method M1.
        var test = Delegate.CreateDelegate(typeof(D2), c1, mi1!,
            throwOnBindFailure: false);

        //The variable test will have the value of null if the method
        //fails to bind. This is a result of having the throwOnBindFailure
        //set to false.
        if (test is not null)
        {
            d2 = (D2)test;

            //The same instance of C is used every time the delegate is invoked.
            d2("Hello World!");
            d2("Hi mom!");
        }

        Console.WriteLine("\nAn open instance method.");

        //This scenario demonstrates the provision of another argument in the
        //delegate that passes in the instance that will be used for the method
        //call.
        //Using delegate type D1 but with instance method M1.
        d1 = (D1)Delegate.CreateDelegate(typeof(D1), null, mi1!);

        d1(c1, "Hello, World!");
        d1(new C(5280), "Hi mom!");

        Console.WriteLine("\nAn open static method.");
        //Similar to the first scenario but with static methods.
        //Using delegate type D2 with static method M2.
        d2 = (D2)Delegate.CreateDelegate(typeof(D2), null, mi2!);

        //No instances of C is provided because it is a static method.
        d2("Hello, World!");
        d2("Hi, mom!");

        Console.WriteLine("\nA static method closed over the first argument (String).");

        //Locking in the first argument for the delegate that will be invoked.
        d3 = (D3)Delegate.CreateDelegate(typeof(D3), "Hello, World!", mi2);

        d3();
    }
}