using SimpleGC;

Console.WriteLine("***** GC Basics *****");

//Create a new Car object on the managed heap.
//We are returned a reference to this object ("refToMyCar").
Car refToMyCar = new Car("Zippy", 50);

//The C# dot operator (.) is used to invoke members
//on the object using our reference variable.
Console.WriteLine(refToMyCar.ToString());

Console.WriteLine("\n***** GC Basics *****");

//Print out estimated number of bytes on heap.
Console.WriteLine($"Estimated bytes on heap: {GC.GetTotalMemory(false)}");

//MaxGeneration is zero based, so add 1 for display purposes.
Console.WriteLine($"This OS has {(GC.MaxGeneration + 1)} object generations");

Car refToMyCar2 = new Car("Zippy", 50);
Console.WriteLine(refToMyCar2.ToString());

//Print out generation of refToMyCar object.
Console.WriteLine($"Generation of refToMyCar is: {GC.GetGeneration(refToMyCar2)}");

Console.ReadLine();