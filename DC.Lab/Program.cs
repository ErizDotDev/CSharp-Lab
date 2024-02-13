namespace DC.Lab;

class Program
{
    public static void Main()
    {
        SByte sbyteValue = -120;
        ByteWithSign value = sbyteValue;
        Console.WriteLine(value);

        value = Byte.MaxValue;
        Console.WriteLine(value);
    }
}
