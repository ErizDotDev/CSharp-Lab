using System.Reflection;

namespace DC.Lab;

public delegate void D1(C1 c, string s);
public delegate void D2(string s);
public delegate void D3();

public delegate void D1_2(C2 c);

class Program
{
    static void Main()
    {
        //RunFirstExample();
        RunSecondExample();
    }

    static void RunFirstExample()
    {
        var c1 = new C1(42);

        var mi1 = typeof(C1).GetMethod("M1",
            BindingFlags.Public | BindingFlags.Instance);
        var mi2 = typeof(C1).GetMethod("M2",
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
        d1(new C1(5280), "Hi mom!");

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

    static void RunSecondExample()
    {
        var c1 = new C2(42);
        var c2 = new C2(1491);
        var f = new F();

        D1_2 d;

        var cmi1 = typeof(C2).GetMethod("M1");
        var cmi2 = typeof(C2).GetMethod("M2");
        var cmi3 = typeof(C2).GetMethod("M3");
        var cmi4 = typeof(C2).GetMethod("M4");

        var fmi1 = typeof(F).GetMethod("M1");
        var fmi3 = typeof(F).GetMethod("M3");
        var fmi4 = typeof(F).GetMethod("M4");

        Console.WriteLine("\nAn instance method on any type, with an argument of type C2");

        d = (D1_2)Delegate.CreateDelegate(typeof(D1_2), c1, cmi1!);
        d(c2);
        d = (D1_2)Delegate.CreateDelegate(typeof(D1_2), f, fmi1!);
        d(c2);

        Console.WriteLine("\nAn instance method on C2 with no arguments.");

        d = (D1_2)Delegate.CreateDelegate(typeof(D1_2), null, cmi2!);
        d(c1);

        Console.WriteLine("\nA static method on any type, with an argument of type C2");

        d = (D1_2)Delegate.CreateDelegate(typeof(D1_2), null, cmi3!);
        d(c1);
        d = (D1_2)Delegate.CreateDelegate(typeof(D1_2), null, fmi3!);
        d(c1);

        Console.WriteLine("\nA static method on any type, with an argument of");
        Console.WriteLine("     that type and argument of type C2");

        d = (D1_2)Delegate.CreateDelegate(typeof(D1_2), c1, cmi4!);
        d(c2);

        var test = Delegate.CreateDelegate(typeof(D1_2), f, fmi4, false);
        if (test is not null)
        {
            d = (D1_2)test;
            d(c2);
        }
    }
}