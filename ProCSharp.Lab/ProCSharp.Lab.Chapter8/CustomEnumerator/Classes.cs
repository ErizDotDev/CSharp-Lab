using System.Collections;

namespace CustomEnumerator;

class Car 
{
    //Constant for maximum speed.
    public const int MaxSpeed = 100;

    //Car properties.
    public int CurrentSpeed { get; set; } = 0;
    public string Name { get; set; } = string.Empty;

    //Is the car still operational?
    private bool _carIsDead;

    //A car has-a radio.
    private readonly Radio _theMusicBox = new Radio();

    //Constructors
    public Car() { }
    public Car(string name, int speed)
    {
        CurrentSpeed = speed;
        Name = name;
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
}

class Radio 
{
    public void TurnOn(bool on) =>
        Console.WriteLine(on ? "Jamming..." : "Quiet time...");
}

//Garage contains a set of Car objects.
class Garage : IEnumerable
{
    //System.Array already implements IEnumerator!
    private Car[] carArray = new Car[4];

    //Fill with some Car objects upon startup.
    public Garage()
    {
        carArray[0] = new Car("Rusty", 30);
        carArray[1] = new Car("Clunker", 55);
        carArray[2] = new Car("Zippy", 30);
        carArray[3] = new Car("Fred", 30);
    }

    //Return the array object's IEnumerator.
    //public IEnumerator GetEnumerator() => carArray.GetEnumerator();
    //Standard way of doing it.
    IEnumerator IEnumerable.GetEnumerator() => carArray.GetEnumerator();
}
