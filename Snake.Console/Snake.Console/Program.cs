// https://github.com/Skymile/SC2025
// https://forms.gle/35mRahLQ1FTsMTq48
// https://discord.gg/pvfhyzmJ
// Visual Studio // JetBrains Rider


// Configuration
using System.ComponentModel.Design;

const int MapWidth = 30;
const int MapHeight = 20;

var snake = new LinkedList<Point>();
for (int i = 0; i < 5; i++)
    snake.AddLast(new Point(4, 5));

var dir = Direction.Right;
Console.CursorVisible = false;
Point apple = new(3, 3);
//

while (true)
{
    // User input
    dir = Console.ReadKey(true).Key switch
    {
        ConsoleKey.D or ConsoleKey.RightArrow 
             when dir != Direction.Left
                => Direction.Right,
        ConsoleKey.A or ConsoleKey.LeftArrow  
             when dir != Direction.Right
                => Direction.Left,
        ConsoleKey.W or ConsoleKey.UpArrow
             when dir != Direction.Down
                => Direction.Up,
        ConsoleKey.S or ConsoleKey.DownArrow  
             when dir != Direction.Up
                => Direction.Down,
        _ => dir
    };

    // Application Logic
    snake = Move(snake, dir);
    var head = snake.First!.Value;

    if (head.X == apple.X && head.Y == apple.Y)
    {
        snake.AddLast(snake.Last!.Value);

        int x = 0, y = 0;
        do
        {
            x = Random.Shared.Next(1, MapWidth - 1);
            y = Random.Shared.Next(1, MapHeight - 1);
        } while (snake.Any(i => i.X == x && i.Y == y));

        apple = new(x, y);
    }

    bool isInRange = snake
        .All(i => 
            i.X >= 1 && 
            i.Y >= 1 &&
            i.X < MapWidth &&
            i.Y < MapHeight
        );

    if (!isInRange)
        break;

    // User Interface
    Console.Clear();
    string wallLine = new('#', MapWidth * 2 + 1);
    Console.Write(wallLine);
    Console.SetCursorPosition(0, MapHeight);
    Console.Write(wallLine);
    for (int i = 1; i < MapHeight; i++)
    {
        Console.SetCursorPosition(0, i);
        Console.Write('#');
        Console.SetCursorPosition(MapWidth * 2, i);
        Console.Write('#');
    }

    foreach (Point p in snake)
    {
        Console.SetCursorPosition(p.X * 2, p.Y);
        Console.Write('o');
    }
    Console.SetCursorPosition(head.X * 2, head.Y);
    Console.Write('O');

    Console.SetCursorPosition(apple.X * 2, apple.Y);
    Console.Write('@');
}

static LinkedList<Point> Move(
        LinkedList<Point> points,
        Direction direction
    )
{
    points.RemoveLast();
    var head = points.First!.Value;
    points.AddFirst(
        direction switch
        {
            Direction.Left  => 
                new Point(head.X - 1, head.Y),
            Direction.Right => 
                new Point(head.X + 1, head.Y),
            Direction.Up    => 
                new Point(head.X, head.Y - 1),
            _ => 
                new Point(head.X, head.Y + 1)
        }
    );
    return points;
}

enum Direction { Right, Left, Up, Down }

record struct Point(int X, int Y);

