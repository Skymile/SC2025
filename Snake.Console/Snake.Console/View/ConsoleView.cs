public class ConsoleView : IViewService
{
    public ConsoleView() =>
        Console.CursorVisible = false;

    public void Display(
        IEnumerable<Point> snake,
        IEnumerable<Point> apples,
        IEnumerable<Point> walls,
        Point snakeLast,
        Point snakeHead)
    {
        Console.SetCursorPosition(snakeLast.X * 2, snakeLast.Y);
        Console.Write(' ');

        foreach (var p in snake)
        {
            Console.SetCursorPosition(p.X * 2, p.Y);
            Console.Write('o');
        }
        Console.SetCursorPosition(snakeHead.X * 2, snakeHead.Y);
        Console.Write('O');

        foreach (var p in apples)
        {
            Console.SetCursorPosition(p.X * 2, p.Y);
            Console.Write('@');
        }

        foreach (var p in walls)
        {
            Console.SetCursorPosition(p.X * 2, p.Y);
            Console.Write('#');
        }
    }
}