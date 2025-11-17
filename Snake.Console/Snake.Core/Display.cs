
using Snake;

public class Display : IDisplay
{
    public void Print(
            IEnumerable<Point> snake,
            int mapWidth,
            int mapHeight,
            Point apple
        )
    {
        var head = snake.First();
        Console.Clear();
        string wallLine = new('#', mapWidth * 2 + 1);
        Console.Write(wallLine);
        Console.SetCursorPosition(0, mapHeight);
        Console.Write(wallLine);
        for (int i = 1; i < mapHeight; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write('#');
            Console.SetCursorPosition(mapWidth * 2, i);
            Console.Write('#');
        }

        foreach (Point p in snake)
        {
            Console.SetCursorPosition(p.X * 2, p.Y);
            Console.Write('o');
        }
        Console.SetCursorPosition(head.X * 2, head.Y);
        Console.Write('O');

        Console.SetCursorPosition(apple.X * 2, apple.Y);
        Console.Write('@');
    }
}