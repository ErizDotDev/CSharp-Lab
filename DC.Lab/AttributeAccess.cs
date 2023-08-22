namespace DC.Lab;

[Author("D. dela Cruz")]
public class FirstDemoFirstTestClass
{ }

public class FirstDemoSecondTestClass
{ }

[Author("J.K. Rowling"), Author("C.S. Lewis")]
public class FirstDemoThirdTestClass
{ }

public class AttributeAccess
{
    public static void Execute()
    {
        PrintAuthorInfo(typeof(FirstDemoFirstTestClass));
        PrintAuthorInfo(typeof(FirstDemoSecondTestClass));
        PrintAuthorInfo(typeof(FirstDemoThirdTestClass));
    }

    static void PrintAuthorInfo(Type t)
    {
        Console.WriteLine($"Author information for {t}");

        // Using reflection
        var attrs = Attribute.GetCustomAttributes(t);

        // Displaying output
        foreach (var attr in attrs)
        {
            if (attr is AuthorAttribute a)
                Console.WriteLine($"\t{a.GetName()}, version {a.Version:f}");
        }
    }
}
