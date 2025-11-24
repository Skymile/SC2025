using Snake.Core.Models;

public interface IDisplay
{
    void Print(
        IEnumerable<Point> snake,
        Point apple
    );
}
