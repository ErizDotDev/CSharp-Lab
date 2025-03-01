namespace InterfaceNameClash;

//Draw image to form.
public interface IDrawToForm
{
    void Draw();
}

//Draw to buffer in memory.
public interface IDrawToMemory
{
    void Draw();
}


//Render to the printer.
public interface IDrawToPrinter
{
    void Draw();
}