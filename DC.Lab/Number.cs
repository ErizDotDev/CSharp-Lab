namespace DC.Lab;

public struct Number
{
    private int n;

    public Number(int value)
    {
        n = value;
    }

    public int Value => n;

    public override bool Equals(Object obj)
    {
        if (obj is null || !(obj is Number))
            return false;
        else
            return n == ((Number)obj).n;
    }

    public override int GetHashCode() => n;

    public override string ToString() => n.ToString();
}
