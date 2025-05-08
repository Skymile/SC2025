using System.Windows.Controls;
using System.Windows.Media;

public class WpfView : IViewService
{
    private readonly Grid mainGrid;
    private Config? cfg;
    private Dictionary<int, Dictionary<int, Label>> grid = [];

    public WpfView(Grid mainGrid) =>
        this.mainGrid = mainGrid;

    public void Configure(Config cfg)
    {
        this.cfg = cfg;

        for (int x = 0; x < cfg.MapWidth; x++)
            for (int y = 0; y < cfg.MapHeight; y++)
            {
                var label = new Label();
                if (!grid.ContainsKey(x))
                    grid[x] = [];

                mainGrid.Children.Add(label);
                Grid.SetRow(label, x);
                Grid.SetColumn(label, y);
                    
                grid[x][y] = label;
            }
    }

    public void Display(
        IEnumerable<Point> snake,
        IEnumerable<Point> apples,
        IEnumerable<Point> walls,
        Point snakeLast,
        Point snakeHead)
    {
        foreach (var i in grid)
            foreach (var j in i.Value)
            {
                j.Value.Background = Brushes.LightGray;
            }

        foreach (var p in snake)
            Write(p.X, p.Y, CellType.SnakeTail);
        Write(snakeHead.X, snakeHead.Y, CellType.SnakeHead);

        foreach (var p in apples)
            Write(p.X, p.Y, CellType.Apple);

        foreach (var p in walls)
            Write(p.X, p.Y, CellType.Wall);
    }

    public enum CellType
    {
        None,
        SnakeHead,
        SnakeTail,
        Wall,
        Apple,
    }

    private void Write(int x, int y, CellType type) =>
        grid[x][y].Background = type switch
        {
            CellType.None      => Brushes.LightGray,
            CellType.SnakeHead => Brushes.Orange,
            CellType.SnakeTail => Brushes.OrangeRed,
            CellType.Wall      => Brushes.Black,
            CellType.Apple     => Brushes.Green,
            _ => throw new NotImplementedException()
        };
}