using System.Windows;
using System.Windows.Input;

namespace Snake.Desktop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            input = new Input<Key>(new DesktopKeyMapper());
            output = new Display(this, MapWidth, MapHeight);
            board = new Board();
            output.Print(board.GetSnake(), MapWidth, MapHeight, board.Apple);

            if (!IsDebug)
                Dispatcher.BeginInvoke(async () =>
                {
                    while (true)
                    {
                        await Task.Delay(TimeoutInMs);
                        Step();
                    }
                });
        }

        private void Step()
        {
            board.Move(dir);
            if (board.IsGameOver(MapWidth, MapHeight))
                Application.Current.Shutdown();
            output.Print(board.GetSnake(), MapWidth, MapHeight, board.Apple);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            dir = input.GetDirection(() => e.Key);
            if (IsDebug)
                Step();
        }

        private const bool IsDebug = true;
        private const int TimeoutInMs = 100;
        private const int MapWidth = 30;
        private const int MapHeight = 20;

        private readonly Input<Key> input;
        private readonly Display output;
        private readonly Board board;
        private Direction dir = Direction.Right;
    }
}