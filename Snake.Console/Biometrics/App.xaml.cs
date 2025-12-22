using System.Windows;

namespace Biometrics
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e) =>
            Biometrics.Startup.Initialize()
                .Inject<MainWindow>()
                .Show();
    }
}
