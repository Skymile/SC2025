using Snake;

public class Board
{
    public Board()
    {
        Apple = new Point(3, 3);
        snake = new LinkedList<Point>();
        for (int i = 0; i < 5; i++)
            snake.AddLast(new Point(4, 5));
    }

    public void Move(Direction direction)
    {
        snake.RemoveLast();
        var head = snake.First!.Value;
        snake.AddFirst(
            direction switch
            {
                Direction.Left =>
                    new Point(head.X - 1, head.Y),
                Direction.Right =>
                    new Point(head.X + 1, head.Y),
                Direction.Up =>
                    new Point(head.X, head.Y - 1),
                _ =>
                    new Point(head.X, head.Y + 1)
            }
        );
    }

    public bool IsGameOver(int mapWidth, int mapHeight)
    {
        var head = snake.First!.Value;

        if (head.X == Apple.X && head.Y == Apple.Y)
        {
            snake.AddLast(snake.Last!.Value);

            int x = 0, y = 0;
            do
            {
                x = Random.Shared.Next(1, mapWidth - 1);
                y = Random.Shared.Next(1, mapHeight - 1);
            } while (snake.Any(i => i.X == x && i.Y == y));

            Apple = new(x, y);
        }

        return !snake
            .All(i =>
                i.X >= 1 &&
                i.Y >= 1 &&
                i.X < mapWidth &&
                i.Y < mapHeight
            );
    }

    public Point Apple { get; private set; }
    
    public IEnumerable<Point> GetSnake() => snake;

    private readonly LinkedList<Point> snake;
}