namespace Snake;

public class Input
{
    public Direction GetDirection() =>
        dir = Console.ReadKey(true).Key switch
        {
            ConsoleKey.D or ConsoleKey.RightArrow
                 when dir != Direction.Left
                    => Direction.Right,
            ConsoleKey.A or ConsoleKey.LeftArrow
                 when dir != Direction.Right
                    => Direction.Left,
            ConsoleKey.W or ConsoleKey.UpArrow
                 when dir != Direction.Down
                    => Direction.Up,
            ConsoleKey.S or ConsoleKey.DownArrow
                 when dir != Direction.Up
                    => Direction.Down,
            _ => dir
        };

    private Direction dir;
}