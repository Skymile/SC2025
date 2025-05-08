
public class Game(IViewService view, Config cfg)
{
    public void Initialize()
    {
        var fullSnake = new List<Point>() { cfg.StartingPosition };
        for (int i = 0; i < board.MapWidth; i++)
            fullSnake.Add(new(i, cfg.StartingPosition.Y));

        walls = board.GetRandomPositions(fullSnake, cfg.WallsCount).ToList();
        apples = board.GetRandomPositions(fullSnake.Concat(walls), cfg.ApplesCount).ToList();
    }

    public async Task GameLoop()
    {
        Point dir = new(1, 0);
        var task = Task.Run(GetDirection);

        Point GetDirection() => new(0, 1);
            //Console.ReadKey(true).Key switch
            //{
            //    ConsoleKey.W => dir.Y > 0 ? dir : new(+0, -1),
            //    ConsoleKey.S => dir.Y < 0 ? dir : new(+0, +1),
            //    ConsoleKey.A => dir.X > 0 ? dir : new(-1, +0),
            //    ConsoleKey.D => dir.X < 0 ? dir : new(+1, +0),
            //    _ => dir
            //};

        while (true)
        {
            if (cfg.IsAsync)
                Thread.Sleep(cfg.SnakeSpeed);
            if (!cfg.IsAsync || task.IsCompleted)
            {
                dir = await task;
                task = Task.Run(GetDirection);
            }

            snake.Move(board, dir);
            if (snake.Positions.Concat(walls)
                    .Count(p => p.X == snake.First.X && p.Y == snake.First.Y) != 1)
                break;

            for (int i = 0; i < apples.Count; i++)
            {
                var apple = apples[i];
                if (apple.X != snake.First.X || apple.Y != snake.First.Y)
                    continue;
                apples.RemoveAt(i);
                snake.Enlarge();

                apples.Add(board
                    .GetRandomPositions(snake.Positions.Concat(walls).Concat(apples), 1)
                    .First()
                );
            }

            var last = snake.Shrink();

            view.Display(
                snake.Positions,
                apples,
                walls,
                last,
                snake.First
            );
        }
    }

    private readonly Board  board = new(cfg);
    private readonly Snake  snake = new();

    private List<Point> walls  = [];
    private List<Point> apples = [];
}
