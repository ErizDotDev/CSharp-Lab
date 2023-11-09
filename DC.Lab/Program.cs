namespace DC.Lab;

public interface IDrawingObject
{
    // Raise this event before drawing the object.
    event EventHandler OnDraw;
}

public interface IShape
{
    // Raise this event after drawing the shape.
    event EventHandler OnDraw;
}

// Base class event publisher inherits two interfaces, each with an
// OnDraw event
public class Shape : IDrawingObject, IShape
{
    // Create an event for each interface event
    event EventHandler? PreDrawEvent;
    event EventHandler? PostDrawEvent;

    object objectLock = new object();

    // Explicit interface implementation required.
    // Associate IDrawingObject's event with PerDrawEvent
    event EventHandler IDrawingObject.OnDraw
    {
        add
        {
            lock (objectLock)
            {
                PreDrawEvent += value;
            }
        }
        remove
        {
            lock (objectLock)
            {
                PreDrawEvent -= value;
            }
        }
    }

    // Explicit interface implementation required.
    // Associate IDrawingObject's event with PerDrawEvent
    event EventHandler IShape.OnDraw
    {
        add
        {
            lock (objectLock)
            {
                PostDrawEvent += value;
            }
        }
        remove
        {
            lock (objectLock)
            {
                PostDrawEvent -= value;
            }
        }
    }

    // For the sake of simplicity this one method implements both interfaces
    public void Draw()
    {
        // Raise IDrawingObject's event before the object is drawn.
        PreDrawEvent?.Invoke(this, EventArgs.Empty);

        Console.WriteLine("Drawing a shape.");

        // Raise IShape's event after the object is drawn.
        PostDrawEvent?.Invoke(this, EventArgs.Empty);
    }
}

public class Subscriber1
{
    // References to the shape object as an IDrawingObject
    public Subscriber1(Shape shape)
    {
        IDrawingObject d = (IDrawingObject)shape;
        d.OnDraw += d_OnDraw!;
    }

    void d_OnDraw(object sender, EventArgs e)
    {
        Console.WriteLine("Sub1 receives the IDrawingObject event.");
    }
}

// References the shape object as an IShape
public class Subscriber2
{
    // References to the shape object as an IDrawingObject
    public Subscriber2(Shape shape)
    {
        IShape d = (IShape)shape;
        d.OnDraw += d_OnDraw!;
    }

    void d_OnDraw(object sender, EventArgs e)
    {
        Console.WriteLine("Sub2 receives the IShape event.");
    }
}

class Program
{
    static void Main()
    {
        Shape shape = new Shape();
        Subscriber1 sub1 = new Subscriber1(shape);
        Subscriber2 sub2 = new Subscriber2(shape);
        shape.Draw();

        Console.WriteLine("Press any key to exit...");
        Console.ReadLine();
    }
}