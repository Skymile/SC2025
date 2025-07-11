﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

public class WpfView : IViewService
{
    private readonly Grid mainGrid;
    private Config? cfg;
    private Dictionary<int, Dictionary<int, Rectangle>> grid = [];

    public WpfView(Grid mainGrid) =>
        this.mainGrid = mainGrid;

    public void Configure(Config cfg)
    {
        this.cfg = cfg;

        for (int i = 0; i < cfg.MapWidth; i++)
            mainGrid.ColumnDefinitions.Add(CreateCol());
        for (int i = 0; i < cfg.MapHeight; i++)
            mainGrid.RowDefinitions.Add(CreateRow());

        for (int x = 0; x < cfg.MapWidth; x++)
            for (int y = 0; y < cfg.MapHeight; y++)
            {
                var rect = new Rectangle();
                if (!grid.ContainsKey(x))
                    grid[x] = [];
                
                mainGrid.Children.Add(rect);
                Grid.SetRow(rect, y);
                Grid.SetColumn(rect, x);
                    
                grid[x][y] = rect;
            }
    }

    private static RowDefinition CreateRow() => new()
    {
        Height = new GridLength(64, GridUnitType.Pixel)
    };

    private static ColumnDefinition CreateCol() => new()
    {
        Width = new GridLength(64, GridUnitType.Pixel)
    };

    public void Display(
        IEnumerable<Point> snake,
        IEnumerable<Point> apples,
        IEnumerable<Point> walls,
        Point snakeLast,
        Point snakeHead)
    {
        foreach (var i in grid)
            foreach (var j in i.Value)
                j.Value.Fill = Brushes.LightGray;

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

    private void Write(int x, int y, CellType type)
    {
        var rect = grid[x][y];

        rect.Fill = type switch
        {
            CellType.None      => Brushes.LightGray,
            CellType.SnakeHead => Brushes.Orange,
            CellType.SnakeTail => Brushes.OrangeRed,
            CellType.Wall      => Brushes.Black,
            CellType.Apple     => Brushes.Green,
            _                  => throw new NotImplementedException()
        };

        rect.RadiusX = rect.RadiusY =
            type switch
            {
                CellType.SnakeHead => 32,
                CellType.SnakeTail => 32,
                CellType.Wall      => 4,
                CellType.Apple     => 32,
                _ => 0
            };
    }
}