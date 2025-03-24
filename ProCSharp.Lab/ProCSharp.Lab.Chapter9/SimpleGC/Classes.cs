namespace SimpleGC;

public class Car
{
    public int CurrentSpeed { get; set; }
    public string Name { get; set; }

    public Car() { }
    public Car(string name, int speed)
    {
        CurrentSpeed = speed;
        Name = name;
    }

    public override string ToString() => $"{Name} is going {CurrentSpeed} MPH";
}
