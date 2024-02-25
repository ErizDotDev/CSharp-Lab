namespace DC.Lab;

public struct Point3
{
    private int x;
    private int y;

    public Point3(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override bool Equals(Object obj)
    {
        if (!(obj is Point3))
            return false;

        var p = (Point3)obj;
        return x == p.x && y == p.y;
    }

    public override int GetHashCode() => Tuple.Create(x, y).GetHashCode();
}
