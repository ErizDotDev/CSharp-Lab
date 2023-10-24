namespace DC.Lab;

public sealed class Person
{
    public int Age { get; set; }

    public string Name { get; set; }

    // Copy constructor
    public Person(Person previousPerson)
    {
        Name = previousPerson.Name;
        Age = previousPerson.Age;
    }

    //// Alternate copy constructor calls the instance constructor
    //public Person(Person previousPerson)
    //    : this(previousPerson.Name, previousPerson.Age)
    //{
    //}

    // Instance constructor
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public string Details() => $"{Name} is {Age.ToString()}";
}

class Program
{
    static void Main()
    {
        // Create a Person object by using the instance constructor.
        var person1 = new Person("George", 40);

        // Create another Person object, copying person1.
        var person2 = new Person(person1);

        // Change each person's age
        person1.Age = 39;
        person2.Age = 42;

        // Change person2's name
        person2.Name = "Charles";

        // Show details to verify that the name and the age fields are distinct.
        Console.WriteLine(person1.Details());
        Console.WriteLine(person2.Details());

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}