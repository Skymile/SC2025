var ioc = new IoC()
    .RegisterSingleton<IInputService>(new InputService())
    .RegisterSingleton<IViewService>(new ConsoleView())
    .RegisterSingleton<Config>()
    .RegisterSingleton<Game>();

var game = ioc.GetService<Game>();

game.Initialize();

await game.GameLoop();
