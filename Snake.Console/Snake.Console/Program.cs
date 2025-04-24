int SnakeSpeed = 100;
int WallsCount = 20;
int ApplesCount = 4;
Point StartingPosition = new(4, 5);
bool isAsync = true;
Console.CursorVisible = false;

var board = new Board();
var snake = new Snake();
var view = new View();

Point dir = new(1, 0);
var fullSnake = new List<Point>() { StartingPosition };
for (int i = 0; i < board.MapWidth; i++)
    fullSnake.Add(new(i, StartingPosition.Y));

var walls = board.GetRandomPositions(fullSnake, WallsCount).ToList();
var apples = board.GetRandomPositions(fullSnake.Concat(walls), ApplesCount).ToList();

Point GetDirection() =>
    Console.ReadKey(true).Key switch
    {
        ConsoleKey.W => dir.Y > 0 ? dir : new(+0, -1),
        ConsoleKey.S => dir.Y < 0 ? dir : new(+0, +1),
        ConsoleKey.A => dir.X > 0 ? dir : new(-1, +0),
        ConsoleKey.D => dir.X < 0 ? dir : new(+1, +0),
        _ => dir
    };

var task = Task.Run(GetDirection);

while (true)
{
    if (isAsync)
        Thread.Sleep(SnakeSpeed);
    if (!isAsync || task.IsCompleted)
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
