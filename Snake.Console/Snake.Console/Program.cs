// https://github.com/Skymile/SC2025
// https://forms.gle/35mRahLQ1FTsMTq48
// https://discord.gg/pvfhyzmJ
// Visual Studio // JetBrains Rider

using System.Net.NetworkInformation;

var snake = new LinkedList<Point>();
snake.AddLast(new Point(4, 5));
snake.AddLast(new Point(5, 5));
snake.AddLast(new Point(6, 5));

Console.Clear();
foreach (Point p in snake)
{
    Console.SetCursorPosition(p.X * 2, p.Y);
    Console.Write('o');
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
                new Point(head.X, head.Y + 1),
            _ => 
                new Point(head.X, head.Y - 1)
        }
    );
    return points;
}

enum Direction { Right, Left, Up, Down }

record struct Point(int X, int Y);

