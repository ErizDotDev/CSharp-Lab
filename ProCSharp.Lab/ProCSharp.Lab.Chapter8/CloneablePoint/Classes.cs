namespace CloneablePoint;

//A class named Point.
public class Point : ICloneable
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int xPos, int yPos)
    {
        X = xPos;
        Y = yPos;
    }

    public Point() { }

    public override string ToString() => $"X = {X}; Y = {Y}";

    //public object Clone() => new Point(this.X, this.Y);
    //Copy each field of the Point member by member.
    public object Clone() => this.MemberwiseClone();
}
