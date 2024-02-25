namespace DC.Lab;

public struct Point2
{
    private int x;
    private int y;

    public Point2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override bool Equals(Object obj)
    {
        if (!(obj is Point2))
            return false;

        var p = (Point2)obj;
        return x == p.x && y == p.y;
    }

    public override int GetHashCode() => x ^ y;
}
