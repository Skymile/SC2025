using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace Snake.Desktop
{
    public class Display : IDisplay
    {
        public Display(MainWindow window, int mapWidth, int mapHeight)
        {
            var grid = new Grid();
            window.Content = grid;

            for (int i = 0; i < mapWidth; i++)
                grid.ColumnDefinitions.Add(CreateColumnDefinition());
            for (int i = 0; i < mapHeight; i++)
                grid.RowDefinitions.Add(CreateRowDefinition());

            this.cells = new Rectangle[mapWidth, mapHeight];

            for (int i = 0; i < mapWidth; i++)
                for (int j = 0; j < mapHeight; j++)
                {
                    var rect = new Rectangle();

                    Grid.SetColumn(rect, i);
                    Grid.SetRow(rect, j);
                    grid.Children.Add(rect);
                    this.cells[i, j] = rect;
                }

            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
        }

        public void Print(
            IEnumerable<Point> snake,
            int mapWidth,
            int mapHeight,
            Point apple)
        {
            for (int i = 0; i < cells.GetLength(0); i++)
                for (int j = 0; j < cells.GetLength(1); j++)
                    cells[i, j].Fill = Brushes.Gray;
            foreach (var p in snake)
                cells[p.X, p.Y].Fill = Brushes.Cyan;
            cells[apple.X, apple.Y].Fill = Brushes.OrangeRed;
        }

        private static ColumnDefinition CreateColumnDefinition() =>
            new() { Width = new GridLength(CellSize, GridUnitType.Pixel) };
        private static RowDefinition CreateRowDefinition() =>
            new() { Height = new GridLength(CellSize, GridUnitType.Pixel) };

        private const int CellSize = 50;

        private readonly Rectangle[,] cells;
        private readonly int mapWidth;
        private readonly int mapHeight;
    }
}