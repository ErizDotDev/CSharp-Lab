namespace MiInterfaceHierarchy;

//Multiple inheritance for interface types is A-okay.
interface IAnotherDrawable
{
    void Draw();
}

interface IPrintable
{
    void Print();
    void Draw(); // <-- Possible name clash here!
}

//Multiple interface inheritance. OK!
interface IShape : IAnotherDrawable, IPrintable
{
    int GetNumberOfSides();
}