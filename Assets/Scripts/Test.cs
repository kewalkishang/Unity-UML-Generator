using System;

public interface IShape
{
    void Draw();
}

public class Circle : IShape
{
    public void Draw()
    {
        Console.WriteLine("Drawing a circle.");
    }
}

public class Rectangle
{
    public void Draw()
    {
        Console.WriteLine("Drawing a rectangle.");
    }
}
