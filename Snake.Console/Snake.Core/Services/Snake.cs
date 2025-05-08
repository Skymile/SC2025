
public class Snake
{
    public Snake(int x = 4, int y = 5, int count = 6)
    {
        for (int i = 0; i< count; i++)
            positions.AddLast(new Point(x, y));
    }

    public Point Shrink() 
    { 
        var last  = positions.Last!.Value;
        positions.RemoveLast();
        return last;
    }

    public void Enlarge() =>
        positions.AddFirst(positions.First!.Value);

    public void Move(Board board, Point dir)
    {
        var first = positions.First!.Value;

        positions.AddFirst(new Point(
            (first.X + dir.X + board.MapWidth)  % board.MapWidth,
            (first.Y + dir.Y + board.MapHeight) % board.MapHeight
        ));
    }

    public Point First => positions.First!.Value;
    public IEnumerable<Point> Positions => positions;

    private readonly LinkedList<Point> positions = [];
}
