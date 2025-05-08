
public class InputService : IInputService
{
    public Point Direction { get; set; } = new Point(1, 0);

    public Point GetDirection() => Direction;
}
