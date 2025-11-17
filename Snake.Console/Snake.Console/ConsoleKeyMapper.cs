using Snake.Core.Enums;
using Snake.Core.Interfaces;

namespace Snake;

public class ConsoleKeyMapper : IKeyMapper<ConsoleKey>
{
    public UserAction? ToUserAction(ConsoleKey key) =>
        key switch
        {
            ConsoleKey.D or ConsoleKey.RightArrow => UserAction.GoRight,
            ConsoleKey.A or ConsoleKey.LeftArrow => UserAction.GoLeft,
            ConsoleKey.W or ConsoleKey.UpArrow => UserAction.GoUp,
            ConsoleKey.S or ConsoleKey.DownArrow => UserAction.GoDown,
            _ => null,
        };
}
