using System.Windows;
using System.Windows.Input;

namespace Snake.Desktop
{
    public class Display : IDisplay
    {
        public void Print(IEnumerable<Point> snake,
                          int mapWidth,
                          int mapHeight,
                          Point apple)
        {

        }
    }

    public partial class MainWindow : Window
    {
        private const int MapWidth = 30;
        private const int MapHeight = 20;
        private Input<Key> input;
        private readonly Display output;
        private readonly Board board;

        public MainWindow()
        {
            InitializeComponent();
            input = new Input<Key>(new DesktopKeyMapper());
            output = new Display();
            board = new Board();
            output.Print(board.GetSnake(), MapWidth, MapHeight, board.Apple);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            input.GetDirection(() => e.Key);
        }
    }
}