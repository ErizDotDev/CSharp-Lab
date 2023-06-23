namespace DC.Lab
{
    class Program
    {
        static int State = 0;

        static void Main(string[] args)
        {
            Console.WriteLine($"Value of State before assignment: {State}");

            SetState();

            Console.WriteLine($"Value of State after reassignment: {State}");
        }

        static void SetState()
        {
            State = 1;
        }
    }
}