namespace DC.Lab;

public class CustomJoin
{
    static List<Category> categories = new()
    {
        new("Beverages", 001),
        new("Condiments", 002),
        new("Vegetables", 003)
    };

    static List<Product> products = new()
    {
        new("Tea", 001),
        new("Mustard", 002),
        new("Pickles", 002),
        new("Carrots", 003),
        new("Bok Choy", 003),
        new("Peaches", 005),
        new("Melons", 005),
        new("Ice Cream", 007),
        new("Mackerel", 012)
    };

    public static void Execute()
    {
        ExecuteCrossJoin();
        ExecuteNonEquijoin();
    }

    private static void ExecuteCrossJoin()
    {
        Console.WriteLine("Executing cross join");

        var crossJoinQuery =
            from c in categories
            from p in products
            select new
            {
                c.ID,
                p.Name
            };

        foreach (var v in crossJoinQuery)
        {
            Console.WriteLine($"{v.ID,5}{v.Name}");
        }
    }

    private static void ExecuteNonEquijoin()
    {
        Console.WriteLine("\nExecuting non-equijoin");

        var nonEquijoinQuery =
            from p in products
            let catIds =
                from c in categories
                select c.ID
            where catIds.Contains(p.CategoryId) == true
            select new
            {
                Product = p.Name,
                p.CategoryId
            };

        foreach (var v in nonEquijoinQuery)
        {
            Console.WriteLine($"{v.CategoryId,-5}{v.Product}");
        }
    }
}
