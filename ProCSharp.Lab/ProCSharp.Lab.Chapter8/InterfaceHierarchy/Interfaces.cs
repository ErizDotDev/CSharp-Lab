﻿namespace InterfaceHierarchy;

public interface IDrawable
{
    void Draw();
}

public interface IAdvancedDraw: IDrawable
{
    void DrawInBoundingBox(int top, int left, int bottom, int right);
    void DrawUpsideDown();
}
