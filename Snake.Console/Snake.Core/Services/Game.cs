public class Game(IViewService view, Config cfg, IInputService input)
{
    public void Initialize()
    {
        var fullSnake = new List<Point>() { cfg.StartingPosition };
        for (int i = 0; i < board.MapWidth; i++)
            fullSnake.Add(new(i, cfg.StartingPosition.Y));

        walls = board.GetRandomPositions(fullSnake, cfg.WallsCount).ToList();
        apples = board.GetRandomPositions(fullSnake.Concat(walls), cfg.ApplesCount).ToList();
    }

    public async Task Step(Point? overridenDirection = null)
    {
        task ??= Task.Run(input.GetDirection);

        if (cfg.IsAsync)
            Thread.Sleep(cfg.SnakeSpeed);
        if (overridenDirection is null && (!cfg.IsAsync || task.IsCompleted))
        {
            dir = await task;
            task = Task.Run(input.GetDirection);
        }
        
        snake.Move(board, overridenDirection ?? dir);
        
        if (snake.Positions.Concat(walls)
                .Count(p => p.X == snake.First.X && p.Y == snake.First.Y) != 1)
        {
            IsFinished = true;
            return;
        }

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

    public async Task GameLoop()
    {
        dir = new(1, 0);
        task = Task.Run(input.GetDirection);

        while (!IsFinished)
            await Step();
    }

    public bool IsFinished { get; set; }

    private readonly Board  board = new(cfg);
    private readonly Snake  snake = new();

    private List<Point> walls  = [];
    private List<Point> apples = [];
    private Point dir;
    private Task<Point> task;
}
