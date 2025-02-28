namespace CustomInterfaces;

class Hexagon : Shape, IPointy
{
    public byte Points => 6;

    public Hexagon() { }

    public override void Draw()
    {
        Console.WriteLine("Drawing the Hexagon");
    }
}
