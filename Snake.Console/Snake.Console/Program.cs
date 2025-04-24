int MapWidth = 20;
int MapHeight = 15;
int SnakeSpeed = 100;
int SnakeCount = 6;
int WallsCount = 180;
int ApplesCount = 4;
(int X, int Y) StartingPosition = (4, 5);
bool isAsync = true;
Console.CursorVisible = false;

(int X, int Y) dir = (1, 0);
var snake = new LinkedList<(int X, int Y)>();
for (int i = 0; i < SnakeCount; i++)
    snake.AddLast(StartingPosition);
var fullSnake = new List<(int X, int Y)>() { StartingPosition };
for (int i = 0; i < MapWidth; i++)
    fullSnake.Add((i, StartingPosition.Y));

var walls = GetRandomPositions(fullSnake, WallsCount).ToList();
var apples = GetRandomPositions(fullSnake.Concat(walls), ApplesCount).ToList();

IEnumerable<(int X, int Y)> GetRandomPositions(
    IEnumerable<(int X, int Y)> taken, int count)
{
    var dict = taken
        .GroupBy(i => i.X)
        .ToDictionary(i => i.Key, i => i.Select(i => i.Y).ToHashSet());

    List<(int X, int Y)> freeCells = [];
    for (int x = 0; x < MapWidth; x++)
        for (int y = 0; y < MapHeight; y++)
        {
            if (dict.TryGetValue(x, out var takenY) &&
                takenY.Contains(y))
                continue;

            freeCells.Add((x, y));
        }

    for (int r = 0; r < count; r++)
    {
        int number = Random.Shared.Next(0, freeCells.Count);
        var found = freeCells[number];
        freeCells.RemoveAt(number);
        yield return found;
    }
}

(int X, int Y) GetDirection() =>
    Console.ReadKey(true).Key switch
    {
        ConsoleKey.W => dir.Y > 0 ? dir : (+0, -1),
        ConsoleKey.S => dir.Y < 0 ? dir : (+0, +1),
        ConsoleKey.A => dir.X > 0 ? dir : (-1, +0),
        ConsoleKey.D => dir.X < 0 ? dir : (+1, +0),
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

    var last = snake.Last!.Value;
    var first = snake.First!.Value;
    snake.AddFirst((
        (first.X + dir.X + MapWidth ) % MapWidth , 
        (first.Y + dir.Y + MapHeight) % MapHeight
    ));
    first = snake.First!.Value;
    if (snake.Concat(walls)
            .Count(p => p.X == first.X && p.Y == first.Y) != 1)
        break;

    for (int i = 0; i < apples.Count; i++)
    {
        var apple = apples[i];
        if (apple.X != first.X || apple.Y != first.Y)
            continue;
        apples.RemoveAt(i);
        snake.AddFirst(first);

        int x = Random.Shared.Next(0, MapWidth);
        int y = Random.Shared.Next(0, MapHeight);
        apples.Add((x, y));
    }

    snake.RemoveLast();

    Console.SetCursorPosition(last.X * 2, last.Y);
    Console.Write(' ');

    foreach (var p in snake)
    {
        Console.SetCursorPosition(p.X * 2, p.Y);
        Console.Write('o');
    }
    Console.SetCursorPosition(first.X * 2, first.Y);
    Console.Write('O');

    foreach (var p in apples)
    {
        Console.SetCursorPosition(p.X * 2, p.Y);
        Console.Write('@');
    }

    foreach (var p in walls)
    {
        Console.SetCursorPosition(p.X * 2, p.Y);
        Console.Write('#');
    }
}
