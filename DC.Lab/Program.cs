namespace DC.Lab;

class Program
{
    public static void Main()
    {
        ByteWithSignE value;

        try
        {
            int intValue = -120;
            value = (ByteWithSignE)intValue;
            Console.WriteLine(value);
        }
        catch (OverflowException e)
        {
            Console.WriteLine(e.Message);
        }

        try
        {
            uint uintValue = 1024;
            value = (ByteWithSignE)uintValue;
            Console.WriteLine(value);
        }
        catch (OverflowException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}