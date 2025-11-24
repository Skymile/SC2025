using Snake.Core.Models;

public class Display(IConfig cfg) : IDisplay
{
    public void Print(
            IEnumerable<Point> snake,
            Point apple
        )
    {
        var head = snake.First();
        Console.Clear();
        string wallLine = new('#', cfg.MapWidth * 2 + 1);
        Console.Write(wallLine);
        Console.SetCursorPosition(0, cfg.MapHeight);
        Console.Write(wallLine);
        for (int i = 1; i < cfg.MapHeight; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write('#');
            Console.SetCursorPosition(cfg.MapWidth * 2, i);
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