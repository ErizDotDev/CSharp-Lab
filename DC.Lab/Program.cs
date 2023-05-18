using System.Collections;

TwoDPoint pointA = new TwoDPoint(3, 4);
TwoDPoint pointB = new TwoDPoint(3, 4);
int i = 5;

Console.WriteLine($"pointA.Equals(pointB) = {pointA.Equals(pointB)}");
Console.WriteLine($"pointA == pointB = {pointA == pointB}");
Console.WriteLine($"object.Equals(pointA, pointB) = {object.Equals(pointA, pointB)}");
Console.WriteLine($"pointA.Equals(null) = {pointA.Equals(null)}");
Console.WriteLine($"pointA == null = {pointA == null}");
Console.WriteLine($"pointA != null = {pointA != null}");
Console.WriteLine($"pointA.Equals(i) = {pointA.Equals(i)}");

ArrayList list = new ArrayList();
list.Add(new TwoDPoint(3, 4));
Console.WriteLine($"pointA.Equals(list[0]) = {pointA.Equals(list[0])}");

TwoDPoint? pointC = null;
TwoDPoint? pointD = null;

Console.WriteLine($"pointA == (pointC = null) = {pointA == pointC}");
Console.WriteLine($"pointC == pointD = {pointC == pointD}");

TwoDPoint temp = new TwoDPoint(3, 4);
pointC = temp;
Console.WriteLine($"pointA == (pointC = 3,4) = {pointA == pointC}");

pointD = temp;
Console.WriteLine($"pointD == (pointC = 3,4) = {pointD == pointC}");

Console.WriteLine("Press any key to exit");
Console.ReadKey();

struct TwoDPoint : IEquatable<TwoDPoint>
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public TwoDPoint(int x, int y)
        : this()
    {
        if (x is (< 1 or > 2000) || y is (< 1 or > 2000))
            throw new ArgumentException("Point must be in range 1 - 2000");

        X = x;
        Y = y;
    }

    public override bool Equals(object? obj) => obj is TwoDPoint other && this.Equals(other);

    public bool Equals(TwoDPoint p) => X == p.X && Y == p.Y;

    public override int GetHashCode() => (X, Y).GetHashCode();

    public static bool operator ==(TwoDPoint lhs, TwoDPoint rhs) => lhs.Equals(rhs);

    public static bool operator !=(TwoDPoint lhs, TwoDPoint rhs) => !(lhs == rhs);
}