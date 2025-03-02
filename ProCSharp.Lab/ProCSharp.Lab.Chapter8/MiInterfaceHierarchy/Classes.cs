namespace MiInterfaceHierarchy;

class Rectangle : IShape
{
    public int GetNumberOfSides() => 4;

    public void Draw() => Console.WriteLine("Drawing...");

    public void Print() => Console.WriteLine("Printing...");
}

class Square : IShape
{
    //Using explicit implementation to handle member name clash.
    void IPrintable.Draw() => Console.WriteLine("Draw to printer...");

    void IAnotherDrawable.Draw() => Console.WriteLine("Draw to screen...");

    public void Print()
    {
        //Print...
    }

    public int GetNumberOfSides() => 4;
}
