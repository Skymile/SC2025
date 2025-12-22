using System.Windows;

using Biometrics.Extensions;
using Biometrics.ViewModels;

namespace Biometrics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowVM vm)
        {
            InitializeComponent();
            this.Title = Localization.Instance.Title;
            vm.RefreshImage = InitializeImage;
            this.DataContext = vm;
            InitializeImage();
        }

        private void InitializeImage() =>
            MainImg.Source = vm.GetImage().ToBitmapSource();

        private MainWindowVM vm => (MainWindowVM)DataContext;
    }
}