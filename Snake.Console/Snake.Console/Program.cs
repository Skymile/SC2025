// https://github.com/Skymile/SC2025
// https://forms.gle/35mRahLQ1FTsMTq48
// https://discord.gg/pvfhyzmJ
// Visual Studio // JetBrains Rider

using Snake;

bool isDebug = false;
const int timeoutInMs = 50;
const int MapWidth = 30;
const int MapHeight = 20;

Console.CursorVisible = false;
var input = new Input<ConsoleKey>(new ConsoleKeyMapper());
var output = new Display();
var board = new Board();

Direction dir = Direction.Right;
void SetDirection() => 
    dir = input.GetDirection(() => Console.ReadKey(true).Key);

Task inputTask;
if (!isDebug)
    inputTask = Task.Run(() =>
    {
        while (true)
            SetDirection();
    });

while (true)
{
    if (isDebug)
        SetDirection();
    await Task.Delay(timeoutInMs);
    board.Move(dir);
    if (board.IsGameOver(MapWidth, MapHeight))
        break;
    output.Print(board.GetSnake(), MapWidth, MapHeight, board.Apple);
}
