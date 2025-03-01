namespace InterfaceHierarchy;

public interface IDrawable
{
    void Draw();
    int TimeToDraw() => 5;
}

public interface IAdvancedDraw: IDrawable
{
    void DrawInBoundingBox(int top, int left, int bottom, int right);
    void DrawUpsideDown();
    new int TimeToDraw() => 15;
}
