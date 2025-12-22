using Biometrics.DI;
using Biometrics.Services;
using Biometrics.ViewModels;

namespace Biometrics
{
    public static class Startup
    {
        public static IIoC Initialize()
        {
            var ioc = new IoC();

            return ioc
                .Set(IoCLifetime.Singleton)
                    .Register<IIoC, IoC>(ioc)
                    .Register<IAlgorithmService, AlgorithmService>()
                    .Register<MainWindowVM, MainWindowVM>()
                    .Register<MainWindow, MainWindow>()

                    ;
        }
    }
}
