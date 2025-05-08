public class InputService : IInputService
{
    public Config? Cfg { get; set; }
    public Game? Game { get; set; }

	public Point Direction
    {
        get => direction;
        set 
        {
            direction = value; 
            if (Cfg?.IsAsync != true)
                Game?.Step(direction);
        }
    }

    public Point GetDirection() => Direction;

    private Point direction = new(1, 0);
}
