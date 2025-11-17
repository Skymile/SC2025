// https://github.com/Skymile/SC2025
// https://forms.gle/35mRahLQ1FTsMTq48
// https://discord.gg/pvfhyzmJ
// Visual Studio // JetBrains Rider

using Snake;
using Snake.Core.Enums;
using Snake.Core.Interfaces;
using Snake.Core.Services;

Console.CursorVisible = false;

var ioc = new IoC()
    .RegisterSingleton<Display, Display>()
    .RegisterSingleton<Config, Config>()
    .RegisterSingleton<Board, Board>()
    .RegisterSingleton<IKeyMapper<ConsoleKey>, ConsoleKeyMapper>();

var cfg    = ioc.Inject<Config>();
var output = ioc.Inject<Display>();
var board  = ioc.Inject<Board>();
var input  = new InputService<ConsoleKey>(ioc.Inject<IKeyMapper<ConsoleKey>>());

Direction dir = Direction.Right;
void SetDirection() => 
    dir = input.GetDirection(() => Console.ReadKey(true).Key);

Task inputTask;
if (!cfg.IsDebug)
    inputTask = Task.Run(() =>
    {
        while (true)
            SetDirection();
    });

while (true)
{
    if (cfg.IsDebug)
        SetDirection();
    await Task.Delay(cfg.TimeoutInMs);
    board.Move(dir);
    if (board.IsGameOver(cfg.MapWidth, cfg.MapHeight))
        break;
    output.Print(board.GetSnake(), cfg.MapWidth, cfg.MapHeight, board.Apple);
}
