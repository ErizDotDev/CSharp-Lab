using SimpleGC;

Console.WriteLine("***** GC Basics *****");

//Create a new Car object on the managed heap.
//We are returned a reference to this object ("refToMyCar").
Car refToMyCar = new Car("Zippy", 50);

//The C# dot operator (.) is used to invoke members
//on the object using our reference variable.
Console.WriteLine(refToMyCar.ToString());

Console.WriteLine("\n***** Fun with System.GC *****");

//Print out estimated number of bytes on heap.
Console.WriteLine($"Estimated bytes on heap: {GC.GetTotalMemory(false)}");

//MaxGeneration is zero based, so add 1 for display purposes.
Console.WriteLine($"This OS has {(GC.MaxGeneration + 1)} object generations");

Car refToMyCar2 = new Car("Zippy", 50);
Console.WriteLine(refToMyCar2.ToString());

//Print out generation of refToMyCar object.
Console.WriteLine($"Generation of refToMyCar is: {GC.GetGeneration(refToMyCar2)}");

//Make a ton of objects for testing purposes.
object[] tonsOfObjects = new object[50000];

for (int i = 0; i < 50000; i++)
{
    tonsOfObjects[i] = new object();
}

//Collect only gen 0 objects.
Console.WriteLine("Force Garbage Collection");
GC.Collect(0, GCCollectionMode.Forced);
GC.WaitForPendingFinalizers();

//Print out generation of refToMyCar object.
Console.WriteLine($"Generation of refToMyCar is: {GC.GetGeneration(refToMyCar2)}");

//See if tonsOfObjects[9000] is still alive.
if (tonsOfObjects[9000] != null)
{
    Console.WriteLine($"Generation of tonsOfObjects[9000] is: {GC.GetGeneration(tonsOfObjects[9000])}");
}
else
{
    Console.WriteLine("tonsOfObjects[9000] is no longer alive.");
}

//Print out how many times a generation has been swept.
for (int i = 0; i < 4; i++)
{
    Console.WriteLine($"Gen {i} has been swept {GC.CollectionCount(i)} times");
}

Console.ReadLine();
