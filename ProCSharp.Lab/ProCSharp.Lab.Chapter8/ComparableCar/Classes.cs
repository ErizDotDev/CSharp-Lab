using System.Collections;

namespace ComparableCar;

//The iteration of the Car can be ordered
//based on the ID.
class Car2 : IComparable
{
    //Constant for maximum speed.
    public const int MaxSpeed = 100;

    //Car properties.
    public int Id { get; set; }
    public int CurrentSpeed { get; set; } = 0;
    public string Name { get; set; } = string.Empty;

    //Is the car still operational?
    private bool _carIsDead;

    //A car has-a radio.
    private readonly Radio2 _theMusicBox = new Radio2();

    //Constructors
    public Car2() { }

    public Car2(string name, int speed)
    {
        CurrentSpeed = speed;
        Name = name;
    }

    public Car2(string name, int currentSpeed, int id)
    {
        Name = name;
        CurrentSpeed = currentSpeed;
        Id = id;
    }

    public void CrankTunes(bool state)
    {
        //Delegate request to inner object.
        _theMusicBox.TurnOn(state);
    }

    //See if Car has overheated.
    public void Accelerate(int delta)
    {
        if (_carIsDead)
        {
            Console.WriteLine($"{Name} is out of order.");
            return;
        }

        CurrentSpeed += delta;
        if (CurrentSpeed > MaxSpeed)
        {
            Console.WriteLine($"{Name} has overheated!");
            CurrentSpeed = 0;
            _carIsDead = true;
            return;
        }

        Console.WriteLine($"=> CurrentSpeed");
    }

    int IComparable.CompareTo(object? obj)
    {
        if (obj is not Car2 temp)
        {
            throw new ArgumentException("Parameter is not a Car!");
        }

        //Long notation
        //if (this.Id > temp.Id)
        //    return 1;

        //if (this.Id < temp.Id)
        //    return -1;

        //return 0;

        //Short notation
        return this.Id.CompareTo(temp.Id);
    }
}

class Radio2
{
    public void TurnOn(bool on) =>
        Console.WriteLine(on ? "Jamming..." : "Quiet time...");
}

//This helper class is used to sort an array of Cars by name.
public class CarNameComparer : IComparer
{
    //Test the name of each object.
    int IComparer.Compare(object? o1, object? o2)
    {
        if (o1 is Car2 t1 && o2 is Car2 t2)
        {
            return string.Compare(t1.Name, t2.Name, StringComparison.CurrentCulture);
        }
        else
        {
            throw new ArgumentException("Parameter is not a Car!");
        }
    }
}