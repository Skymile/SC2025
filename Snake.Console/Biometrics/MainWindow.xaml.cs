using System.IO;
using System.Windows;

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

            fileToPath = Directory.EnumerateFiles("../../../Samples/")
                .ToDictionary(
                    i => Path.GetFileNameWithoutExtension(i)
                        ?? throw new NullReferenceException(),
                    i => i
                );

            Files = [.. Directory.EnumerateFiles("../../../Samples/")
                .Select(Path.GetFileNameWithoutExtension)
                .OfType<string>()];

            MainImg.Source = new Image("../../../Samples/apple.png")
                .Apply(new Grayscale())
                .Apply(new MedianFilter())
                .ToBitmapSource();

            this.DataContext = this;
        }

        public int SelectedFile { get; set; }
        public string[] Files { get; set; }

        private readonly Dictionary<string, string> fileToPath = [];

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) =>
            MainImg.Source = new Image(fileToPath[Files[SelectedFile]])
                .Apply(new Grayscale())
                .Apply(new MedianFilter())
                .ToBitmapSource();
    }
}