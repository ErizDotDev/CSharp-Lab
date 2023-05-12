using DC.Lab;

var list = new GenericList<int>();

for (int x = 0; x < 10; x++)
{
    list.AddHead(x);
}

foreach (int i in list)
{
    Console.Write($"{i} ");
}

Console.WriteLine("Done");
