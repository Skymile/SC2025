using System.Windows;
using System.Windows.Controls;

namespace Snake.Desktop;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        ioc = new IoC()
            .RegisterSingleton<IViewService>(new WpfView(MainGrid))
            .RegisterSingleton<Config>()
            .RegisterSingleton<Game>();
        
        cfg = ioc.GetService<Config>();
        (ioc.GetService<IViewService>() as WpfView)?.Configure(cfg);

        for (int i = 0; i < cfg.MapWidth; i++)
            MainGrid.ColumnDefinitions.Add(CreateCol());
        for (int i = 0; i < cfg.MapHeight; i++)
            MainGrid.RowDefinitions.Add(CreateRow());

        Loop();
    }

    private async void Loop()
    {
        var game = ioc.GetService<Game>();

        game.Initialize();

        await game.GameLoop();
    }

    private static RowDefinition CreateRow() => new()
    {
        Height = new GridLength(64, GridUnitType.Pixel)
    };

    private static ColumnDefinition CreateCol() => new()
    {
        Width = new GridLength(64, GridUnitType.Pixel)
    };

    private readonly Config cfg;
    private readonly IoC ioc = new();
}