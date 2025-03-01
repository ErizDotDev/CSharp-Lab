namespace CustomInterfaces;

class Hexagon : Shape, IPointy, IDraw3D
{
    public byte Points => 6;

    public Hexagon() { }

    public override void Draw() => Console.WriteLine("Drawing the Hexagon");

    public void Draw3D() => Console.WriteLine("Drawing Hexagon in 3D!");
}
