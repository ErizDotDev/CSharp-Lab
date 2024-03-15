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

public class C2
{
    private int id;

    public int Id => id;

    public C2(int id)
    {
        this.id = id;
    }

    public void M1(C2 c)
    {
        Console.WriteLine($"Instance method M1(C2 c) on C2: this.id = {id}, c.Id = {c.Id}");
    }

    public void M2()
    {
        Console.WriteLine($"Instance method M2() on C2: this.id = {id}");
    }

    public static void M3(C2 c)
    {
        Console.WriteLine($"Static method M3(C2 c) on C2: c.Id = {c.Id}");
    }

    public static void M4(C2 c2_1, C2 c2_2)
    {
        Console.WriteLine($"Static method M4(C2 c2_1, C2 c2_2) on C2: c2_1.Id = {c2_1.Id}, c2_2.Id = {c2_2.Id}");
    }
}

public class F
{
    public void M1(C2 c)
    {
        Console.WriteLine($"Instance method M1(C2 c) on F: c.Id = {c.Id}");
    }

    public static void M3(C2 c)
    {
        Console.WriteLine($"Static method M3(C2 c) on F: c.Id = {c.Id}");
    }

    public static void M4(F f, C2 c)
    {
        Console.WriteLine($"Static method M4(F f, C2 c) on C2: c.Id = {c.Id}");
    }
}
