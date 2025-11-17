using Snake.Core.Models;

public interface IDisplay
{
    void Print(
        IEnumerable<Point> snake,
        int mapWidth,
        int mapHeight,
        Point apple
    );
}
