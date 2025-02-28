using CustomInterfaces;

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

Console.ReadLine();
