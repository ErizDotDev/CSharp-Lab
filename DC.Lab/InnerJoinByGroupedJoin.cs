namespace DC.Lab;

public class InnerJoinByGroupedJoin
{
    public static void Execute()
    {
        Person magnus = new(firstName: "Magnus", lastName: "Hedlund");
        Person terry = new("Terry", "Adams");
        Person charlotte = new("Charlotte", "Weiss");
        Person arlene = new("Arlene", "Huff");

        List<Person> people = new() { magnus, terry, charlotte, arlene };

        List<Pet> pets = new()
        {
            new("Barley", terry),
            new("Boots", terry),
            new("Whiskers", charlotte),
            new("Blue Moon", terry),
            new("Daisy", magnus),
        };

        var query1 =
            from person in people
            join pet in pets on person equals pet.Owner into gj
            from subpet in gj
            select new
            {
                OwnerName = person.FirstName,
                PetName = subpet.Name
            };

        Console.WriteLine("Inner join using GroupJoin():");
        foreach (var data in query1)
        {
            Console.WriteLine($"{data.OwnerName} => \t{data.PetName}");
        }

        var query2 =
            from person in people
            join pet in pets on person equals pet.Owner
            select new
            {
                OwnerName = person.FirstName,
                PetName = pet.Name
            };

        Console.WriteLine();
        Console.WriteLine("The equivalent operation using Join():");

        foreach (var data in query2)
        {
            Console.WriteLine($"{data.OwnerName} => \t{data.PetName}");
        }
    }
}

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}

public class Pet
{
    public string Name { get; set; }
    public Person Owner { get; set; }

    public Pet(string name, Person owner)
    {
        Name = name;
        Owner = owner;
    }
}
