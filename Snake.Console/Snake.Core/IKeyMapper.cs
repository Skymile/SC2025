namespace Snake.Core;

public interface IKeyMapper<T>
{
    UserAction? ToUserAction(T key);
}