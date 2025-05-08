
public interface IViewService
{
    void Display(
        IEnumerable<Point> snake, 
        IEnumerable<Point> apples, 
        IEnumerable<Point> walls, 
        Point snakeLast, 
        Point snakeHead);
}