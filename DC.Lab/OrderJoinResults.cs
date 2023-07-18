namespace DC.Lab;

public class OrderJoinResults
{
    public static void Execute()
    {
        List<Category> categories = new()
        {
            new("Beverages", 001),
            new("Condiments", 002),
            new("Vegetables", 003),
            new("Grains", 004),
            new("Fruit", 005)
        };

        List<Product> products = new()
        {
            new("Cola", 001),
            new("Tea", 001),
            new("Mustard", 002),
            new("Pickles", 002),
            new("Carrots", 003),
            new("Bok Choy", 003),
            new("Peaches", 005),
            new("Melons", 005),
        };

        var groupJoinQuery =
            from category in categories
            join prod in products on category.ID equals prod.CategoryId into prodGroup
            orderby category.Name
            select new
            {
                Category = category.Name,
                Products =
                    from prod2 in prodGroup
                    orderby prod2.Name
                    select prod2
            };

        foreach (var productGroup in groupJoinQuery)
        {
            Console.WriteLine(productGroup.Category);

            foreach (var prodItem in productGroup.Products)
            {
                Console.WriteLine($"\t{prodItem.Name,-10} {prodItem.CategoryId}");
            }
        }
    }
}

public record Product(string Name, int CategoryId);
public record Category(string Name, int ID);
