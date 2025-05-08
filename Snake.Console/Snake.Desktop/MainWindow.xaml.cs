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
        input.Game = game;
        input.Cfg = cfg;

        if (cfg.IsAsync)
            while (!game.IsFinished)
            {
                await Task.Delay(100);
                await game.Step(input.Direction);
            }
    }

    private readonly Config cfg;
    private readonly IoC ioc = new();

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.W: input.Direction = new(0, -1); break;
            case Key.S: input.Direction = new(0, +1); break;
            case Key.A: input.Direction = new(-1, 0); break;
            case Key.D: input.Direction = new(+1, 0); break;
        }

        if (game.IsFinished)
            Application.Current.Shutdown();
    }
}