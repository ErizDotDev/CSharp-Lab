namespace CloneablePoint;

//A class named Point.
public class Point : ICloneable
{
    public int X { get; set; }
    public int Y { get; set; }
    public PointDescription Description = new PointDescription();

    public Point(int xPos, int yPos, string name)
    {
        X = xPos;
        Y = yPos;
        Description.Name = name;
    }

    public Point(int xPos, int yPos)
    {
        X = xPos;
        Y = yPos;
    }

    public Point() { }

    public override string ToString() => $"X = {X}; Y = {Y}; Name = {Description.Name}; ID = {Description.Id}";

    //public object Clone() => new Point(this.X, this.Y);
    //Copy each field of the Point member by member.
    //public object Clone() => this.MemberwiseClone();

    //Now we need to adjust for the PointDescriptor member.
    public object Clone()
    {
        //First get a shallow copy.
        var newPoint = (Point)this.MemberwiseClone();

        //Then fill in the gaps.
        var currentDesc = new PointDescription();
        currentDesc.Name = this.Description.Name;
        newPoint.Description = currentDesc;

        return newPoint;
    }
}

//This class describes a point.
public class PointDescription
{
    public string Name { get; set; }
    public Guid Id { get; set; }

    public PointDescription()
    {
        Name = "No-name";
        Id = Guid.NewGuid();
    }
}
