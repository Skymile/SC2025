using System.Windows;
using System.Windows.Input;

namespace Snake.Desktop;

public partial class MainWindow : Window
{
    private InputService input;
    private Game game;

    public MainWindow()
    {
        InitializeComponent();

        ioc = new IoC()
            .RegisterSingleton<IInputService>(input = new InputService())
            .RegisterSingleton<IViewService>(new WpfView(MainGrid))
            .RegisterSingleton<Config>()
            .RegisterSingleton<Game>();

        cfg = ioc.GetService<Config>();
        (ioc.GetService<IViewService>() as WpfView)?.Configure(cfg);

        Loop();
    }

    private async void Loop()
    {
        game = ioc.GetService<Game>();
        game.Initialize();

        if (!cfg.IsAsync)
            return;

        while (!game.IsFinished)
        {
            await Task.Delay(cfg.SnakeSpeed);
            await game.Step(input.Direction);
        }
    }

    private readonly Config cfg;
    private readonly IoC ioc = new();

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.W: Step(new(0, -1)); break;
            case Key.S: Step(new(0, +1)); break;
            case Key.A: Step(new(-1, 0)); break;
            case Key.D: Step(new(+1, 0)); break;
        }
    }

    private async void Step(Point direction)
    {
        ArgumentNullException.ThrowIfNull(game);
        ArgumentNullException.ThrowIfNull(cfg);

        input.Direction = direction;
        if (cfg.IsAsync != true)
            await game.Step(direction);
        if (game.IsFinished)
            Application.Current.Shutdown();
    }
}