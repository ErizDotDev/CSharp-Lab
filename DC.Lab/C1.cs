namespace DC.Lab;

public class C1
{
    private int id;

    public C1(int id)
    {
        this.id = id;
    }

    public void M1(string s)
    {
        Console.WriteLine($"Instance method M1 on C: id = {this.id}, s = {s}");
    }

    public static void M2(string s)
    {
        Console.WriteLine($"Static method M2 on C: s = {s}");
    }
}
