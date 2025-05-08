public class InputService : IInputService
{
    public Point GetDirection() =>
        dir = Console.ReadKey(true).Key switch
        {
            ConsoleKey.W => dir.Y > 0 ? dir : new(+0, -1),
            ConsoleKey.S => dir.Y < 0 ? dir : new(+0, +1),
            ConsoleKey.A => dir.X > 0 ? dir : new(-1, +0),
            ConsoleKey.D => dir.X < 0 ? dir : new(+1, +0),
            _ => dir
        };

    private Point dir;
}
