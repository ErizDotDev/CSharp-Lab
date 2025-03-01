namespace CustomInterfaces;

class Square : Shape, IRegularPointy
{
    //This comes from the IPointy interface.
    public byte Points => 4;

    //These came from the IRegularPointy interface.
    public int SideLength { get; set; }
    public int NumberOfSides { get; set; }
    //Note that the Perimeter property is not implemented.

    public Square() { }

    //Draw comes from the Shape base class.
    public override void Draw()
    {
        Console.WriteLine("Drawing a square.");
    }    
}
