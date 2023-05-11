var person = new Person("Denzel", "dela Cruz");
Console.WriteLine(person);

public record Person(string FirstName, string LastName);