// https://github.com/Skymile/SC2025
// https://discord.gg/JAzZZ3yS

// .NET Framework 4.8
// .NET Core -> 9.0
//    .NET 9.0
// .NET Standard 
// Mono
//
int MapWidth = 20;
int MapHeight = 15;
bool isAsync = true;
Console.CursorVisible = false;

(int X, int Y) dir = (1, 0);
var snake = new LinkedList<(int X, int Y)>();
snake.AddLast((4, 5));
snake.AddLast((4, 5));
snake.AddLast((4, 5));
snake.AddLast((4, 5));
snake.AddLast((4, 5));
snake.AddLast((4, 5));
snake.AddLast((4, 5));

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
        Thread.Sleep(200);

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
    snake.RemoveLast();

    Console.SetCursorPosition(last.X * 2, last.Y);
    Console.Write(' ');

    foreach (var p in snake)
    {
        Console.SetCursorPosition(p.X * 2, p.Y);
        Console.Write('*');
    }
}
