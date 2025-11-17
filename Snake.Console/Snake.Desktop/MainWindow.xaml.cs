using System.Windows;
using System.Windows.Input;

using Snake.Core.Enums;
using Snake.Core.Services;

namespace Snake.Desktop;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        input = new InputService<Key>(new DesktopKeyMapper());
        output = new Display(this, cfg.MapWidth, cfg.MapHeight);
        board = new Board();
        output.Print(board.GetSnake(), cfg.MapWidth, cfg.MapHeight, board.Apple);

        if (!cfg.IsDebug)
            Dispatcher.BeginInvoke(async () =>
            {
                while (true)
                {
                    await Task.Delay(cfg.TimeoutInMs);
                    Step();
                }
            });
    }

    private void Step()
    {
        board.Move(dir);
        if (board.IsGameOver(cfg.MapWidth, cfg.MapHeight))
            Application.Current.Shutdown();
        output.Print(board.GetSnake(), cfg.MapWidth, cfg.MapHeight, board.Apple);
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        dir = input.GetDirection(() => e.Key);
        if (cfg.IsDebug)
            Step();
    }

    private readonly Config cfg = new Config();
    private readonly InputService<Key> input;
    private readonly Display output;
    private readonly Board board;
    private Direction dir = Direction.Right;
}