int MapWidth = 20;
int MapHeight = 15;
int SnakeSpeed = 100;
int SnakeCount = 6;
int WallsCount = 6;
int ApplesCount = 2;
(int X, int Y) StartingPosition = (4, 5);
bool isAsync = true;
Console.CursorVisible = false;

(int X, int Y) dir = (1, 0);
var snake = new LinkedList<(int X, int Y)>();
for (int i = 0; i < SnakeCount; i++)
    snake.AddLast(StartingPosition);
var walls = new List<(int X, int Y)>();
for (int i = 0; i < WallsCount; i++)
{
    int x = Random.Shared.Next(0, MapWidth);
    int y = Random.Shared.Next(0, MapHeight - 1);
    if (y >= StartingPosition.Y) ++y;
    walls.Add((x, y));
}
var apples = new List<(int X, int Y)>();
for (int i = 0; i < ApplesCount; i++)
{
    int x = Random.Shared.Next(0, MapWidth);
    int y = Random.Shared.Next(0, MapHeight - 1);
    if (y >= StartingPosition.Y) ++y;
    apples.Add((x, y));
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
        if (apple.X == first.X && apple.Y == first.Y)
        {
            apples.RemoveAt(i);
            snake.AddFirst(first);

            int x = Random.Shared.Next(0, MapWidth);
            int y = Random.Shared.Next(0, MapHeight);
            apples.Add((x, y));
        }
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

    foreach (var p in walls)
    {
        Console.SetCursorPosition(p.X * 2, p.Y);
        Console.Write('#');
    }

    foreach (var p in apples)
    {
        Console.SetCursorPosition(p.X * 2, p.Y);
        Console.Write('@');
    }
}
