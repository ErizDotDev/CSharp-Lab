using System.Collections;

namespace Bookstore;

public struct Book
{
    public string Title;
    public string Author;
    public decimal Price;
    public bool IsPaperback;

    public Book(string title, string author, decimal price, bool isPaperback)
    {
        Title = title;
        Author = author;
        Price = price;
        IsPaperback = isPaperback;
    }
}

public delegate void ProcessBookCallback(Book book);

public class BookDb
{
    ArrayList list = new ArrayList();

    public void AddBook(string title, string author, decimal price, bool isPaperback)
    {
        list.Add(new Book(title, author, price, isPaperback));
    }

    // Call a passed-in delegate on each paperback book to process it.
    public void ProcessPaperbackBooks(ProcessBookCallback processBook)
    {
        foreach (Book book in list)
        {
            if (book.IsPaperback)
                processBook(book);
        }
    }
}

class PriceTotaller
{
    int booksCount = 0;
    decimal totalBooksPrice = 0.0m;

    internal void AddBookToTotal(Book book)
    {
        booksCount++;
        totalBooksPrice += book.Price;
    }

    internal decimal AveragePrice()
    {
        return totalBooksPrice / booksCount;
    }
}

class Test
{
    static void PrintTitle(Book b)
    {
        Console.WriteLine($"\t{b.Title}");
    }

    static void Main()
    {
        var bookDb = new BookDb();

        AddBooks(bookDb);

        Console.WriteLine("Paperback Book Titles:");

        // Create a new delegate object associated with the static method
        // Test.PrintTitle
        bookDb.ProcessPaperbackBooks(PrintTitle);

        // Get the average price of a paperback by using a PriceTotaller
        // object:
        var totaller = new PriceTotaller();

        // Create a new delegate object associated with the nonstatic method
        // AddBookToTotal on the object totaller:
        bookDb.ProcessPaperbackBooks(totaller.AddBookToTotal);

        Console.WriteLine($"Average Paperback Book Price: ${totaller.AveragePrice():#.##}");
    }

    static void AddBooks(BookDb bookDb)
    {
        bookDb.AddBook("The C Programming Language", "Brian W. Kernighan and Dennis M. Ritchie", 19.95m, true);
        bookDb.AddBook("The Unicode Standard 2.0", "The Unicode Consortium", 39.95m, true);
        bookDb.AddBook("The MS-DOS Encyclopedia", "Ray Duncan", 129.95m, false);
        bookDb.AddBook("Dogbert's Clues for the Clueless", "Scott Adams", 12.00m, true);
    }
}