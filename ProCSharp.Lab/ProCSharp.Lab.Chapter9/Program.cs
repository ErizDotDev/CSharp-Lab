using SimpleGC;

Console.WriteLine("***** GC Basics *****");

//Create a new Car object on the managed heap.
//We are returned a reference to this object ("refToMyCar").
Car refToMyCar = new Car("Zippy", 50);

//The C# dot operator (.) is used to invoke members
//on the object using our reference variable.
Console.WriteLine(refToMyCar.ToString());
Console.ReadLine();
