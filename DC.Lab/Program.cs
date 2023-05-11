var phoneNumbers = new string[2];
var person1 = new Person("Denzel", "dela Cruz", phoneNumbers);
var person2 = new Person("Denzel", "dela Cruz", phoneNumbers);
Console.WriteLine(person1 == person2);

person1.PhoneNumbers[0] = "+63 947 249 0987";
Console.WriteLine(person1 == person2);

Console.WriteLine("Checking why person1 and person2 are equal given the operation in line 6");
Console.WriteLine(person1.PhoneNumbers[0]);
Console.WriteLine(person2.PhoneNumbers[0]);
Console.WriteLine(person1.PhoneNumbers == person2.PhoneNumbers);

Console.WriteLine(ReferenceEquals(person1, person2));

public record Person(string FirstName, string LastName, string[] PhoneNumbers);