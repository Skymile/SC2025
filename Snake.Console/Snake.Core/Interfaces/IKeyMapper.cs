using Snake.Core.Enums;

namespace Snake.Core.Interfaces;

public interface IKeyMapper<T>
{
    UserAction? ToUserAction(T key);
}