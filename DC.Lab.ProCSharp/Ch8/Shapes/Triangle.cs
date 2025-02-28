namespace CustomInterfaces;

class Triangle : Shape, IPointy
{
    public byte Points => 3;

    public Triangle() { }

    public override void Draw()
    {
        Console.WriteLine("Drawing the Triangle");
    }
}
