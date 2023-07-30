namespace DC.Lab;

public class PartitioningData
{
    public static void Execute()
    {
        int chunkNumber = 1;

        foreach (var chunk in Enumerable.Range(0, 8).Chunk(3))
        {
            Console.WriteLine($"Chunk {chunkNumber++}:");

            foreach (int item in chunk)
            {
                Console.WriteLine($"\t{item}");
            }
        }
    }
}
