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
        public MainWindow()
        {
            InitializeComponent();
            this.Title = Localization.Instance.Title;
            this.DataContext = new MainWindowVM();
        }

        public void InitializeImage() =>
            MainImg.Source = vm.GetImage().ToBitmapSource();

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) =>
            InitializeImage();

        private void ComboBoxWindow_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) =>
            InitializeImage();

        private MainWindowVM vm => (MainWindowVM)DataContext;
    }
}