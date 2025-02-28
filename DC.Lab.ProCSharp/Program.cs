using CustomInterfaces;
using System.Security.AccessControl;

Console.WriteLine("***** A First Look at Interfaces *****\n");
CloneableExample();

static void CloneableExample()
{
    //All of these classes support the ICloneable interface.
    string myStr = "Hello";
    var unixOs = new OperatingSystem(PlatformID.Unix, new Version());

    //Therefore, they can all be passed into a method taking ICloneable.
    CloneMe(myStr);
    CloneMe(unixOs);

    static void CloneMe(ICloneable c)
    {
        //Clone whatever we get and print out the name.
        object theClone = c.Clone();
        Console.WriteLine($"Your clone is a: {theClone.GetType().Name}");
    }
}

Console.WriteLine("\n***** Fun with Interfaces *****\n");

var sq = new Square { NumberOfSides = 4, SideLength = 4 };

sq.Draw();

//This won't compile
//Console.WriteLine($"DC has {sq.NumberOfSides} of length {sq.SideLength} and a perimeter of {sq.Perimeter}");

//To resolve, you can do this:
Console.WriteLine($"DC has {sq.NumberOfSides} of length {sq.SideLength} and a perimeter of {((IRegularPointy)sq).Perimeter}");

//You can also resolve this by replacing line 27 with:
//IRegularPointy sq = new Square { NumberOfSides = 4, SideLength = 4 };

Console.WriteLine($"\nExample property: {IRegularPointy.ExampleProperty}");
IRegularPointy.ExampleProperty = "Updated";
Console.WriteLine($"Example property: {IRegularPointy.ExampleProperty}");

Console.ReadLine();
