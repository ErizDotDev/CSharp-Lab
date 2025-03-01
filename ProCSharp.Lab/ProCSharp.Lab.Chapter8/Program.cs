﻿using CustomInterfaces;
using InterfaceHierarchy;
using InterfaceNameClash;

Console.WriteLine("***** A First Look at Interfaces *****\n");
CloneableExample();

static void CloneableExample()
{
    //All of these classes support the ICloneable interface.
    string myStr = "Hello";
    var unixOs = new OperatingSystem(PlatformID.Unix, new Version());

    //Therefore, they can all be passed into a method taking ICloneable.
    CloneMe(myStr);
    CloneMe(unixOs);

    static void CloneMe(ICloneable c)
    {
        //Clone whatever we get and print out the name.
        object theClone = c.Clone();
        Console.WriteLine($"Your clone is a: {theClone.GetType().Name}");
    }
}

Console.WriteLine("\n***** Fun with Interfaces *****\n");

var sq = new Square { NumberOfSides = 4, SideLength = 4 };

sq.Draw();

//This won't compile
//Console.WriteLine($"DC has {sq.NumberOfSides} of length {sq.SideLength} and a perimeter of {sq.Perimeter}");

//To resolve, you can do this:
Console.WriteLine($"DC has {sq.NumberOfSides} of length {sq.SideLength} and a perimeter of {((IRegularPointy)sq).Perimeter}");

//You can also resolve this by replacing line 27 with:
//IRegularPointy sq = new Square { NumberOfSides = 4, SideLength = 4 };

Console.WriteLine($"\nExample property: {IRegularPointy.ExampleProperty}");
IRegularPointy.ExampleProperty = "Updated";
Console.WriteLine($"Example property: {IRegularPointy.ExampleProperty}");

Console.WriteLine();

//I'll draw anyone supporting IDraw3D.
static void DrawIn3D(IDraw3D itf3d)
{
    Console.WriteLine("-> Drawing IDraw3D compatible type");
    itf3d.Draw3D();
}

Shape[] myShapes = {
    new Hexagon(),
    new Circle(),
    new Triangle(),
    new Circle()
};

for (int i = 0; i < myShapes.Length; i++)
{
    //Can I draw you in 3D?
    if (myShapes[i] is IDraw3D s)
    {
        DrawIn3D(s);
    }
}

Console.WriteLine();

//This method returns the first object in the array theat implements IPointy.
static IPointy? FindFirstPointyShape(Shape[] shapes) 
    => shapes.FirstOrDefault(s => s is IPointy) as IPointy;

//Get first pointy item.
var firstPointyItem = FindFirstPointyShape(myShapes);
//To be safe, use the null conditional operator.
Console.WriteLine($"The item has {firstPointyItem?.Points} points");

Console.WriteLine();

//This array can only contain types that implement the IPointy interface.
IPointy[] myPointyObjects = { 
    new Hexagon(),
    new Knife(),
    new Triangle(),
    new Fork(),
    new PitchFork()
};

foreach (IPointy i in myPointyObjects)
{
    Console.WriteLine($"Object has {i.Points} points.");
}

Console.WriteLine("\n***** Fun with Interface Name Clashes *****\n");

var oct = new Octagon();

//We now must use casting to access the Draw() method.
var itfForm = (IDrawToForm)oct;
itfForm.Draw();

//Both of these invocations call the same Draw() method.

//Shorthand notation if you don't need the interface variable for later use.
((IDrawToPrinter)oct).Draw();

//Could also use the is keyword.
if (oct is IDrawToMemory dtm)
{
    dtm.Draw();
}

Console.WriteLine("\n***** Simple Interface Hierarchy *****\n");

//Call from object level.
var myBitmap = new BitmapImage();
myBitmap.Draw();
myBitmap.DrawInBoundingBox(10, 10, 100, 150);
myBitmap.DrawUpsideDown();

//Get IAdvancedDraw explicitly.
if (myBitmap is IAdvancedDraw advDraw)
{
    advDraw.DrawUpsideDown();
}

Console.ReadLine();
