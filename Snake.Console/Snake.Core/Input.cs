using Snake.Core;

namespace Snake;

public class Input<T>(IKeyMapper<T> mapper)
{
    public Direction GetDirection(Func<T> getKey) =>
        dir = mapper.ToUserAction(getKey()) switch
        {
            UserAction.GoRight when dir != Direction.Left  => Direction.Right,
            UserAction.GoLeft  when dir != Direction.Right => Direction.Left,
            UserAction.GoUp    when dir != Direction.Down  => Direction.Up,
            UserAction.GoDown  when dir != Direction.Up    => Direction.Down,
            _ => dir
        };

    private Direction dir;
}