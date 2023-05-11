var person1 = new Person("Denzel", "dela Cruz") { PhoneNumbers = new string[1] };
Console.WriteLine(person1);

var person2 = person1 with { FirstName = "Krystel" };
Console.WriteLine(person2);
Console.WriteLine(person1 == person2);

person2 = person1 with { PhoneNumbers = new string[1] };
Console.WriteLine(person2);
Console.WriteLine(person1 == person2);
Console.WriteLine(ReferenceEquals(person1, person2));

public record Person(string FirstName, string LastName)
{
    // PhoneNumbers is an immutable property
    public string[] PhoneNumbers { get; init; } = new string[2];
}