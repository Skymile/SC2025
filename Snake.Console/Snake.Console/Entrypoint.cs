var ioc = new IoC()
    .RegisterSingleton<IViewService>(new ConsoleView())
    .RegisterSingleton<Game>();

var game = ioc.GetService<Game>();

game.Initialize();

await game.GameLoop();
