namespace DC.Lab;

public class LeftOuterJoin
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

        var query =
            from person in people
            join pet in pets on person equals pet.Owner into gj
            from subpet in gj.DefaultIfEmpty()
            select new
            {
                person.FirstName,
                PetName = subpet?.Name ?? string.Empty
            };

        foreach (var v in query)
        {
            Console.WriteLine($"{v.FirstName + ":",-15}{v.PetName}");
        }
    }
}
