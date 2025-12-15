using System.IO;
using System.Reflection;
using System.Windows;

using Biometrics.Core;
using Biometrics.Extensions;
using Biometrics.ImageProcessing;

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
            this.DataContext = this;
        }

        public void InitializeImage(string path, double[] matrix) =>
            MainImg.Source = new Image("../../../Samples/apple.png")
                .Apply(new Grayscale())
                .Apply(new MedianFilter())
                .Apply(new MedianFilter())
                .Apply(new ConvolutionFilter(matrix)
                {
                    Sensitivity = 1.0
                })
                .ToBitmapSource();

        public int SelectedFile { get; set; }
        public string[] Files { get; set; }

        public int SelectedWindow { get; set; }
        public string[] Windows { get; set; } = windowDict.Keys.ToArray();

        private static readonly Dictionary<string, double[]> windowDict = typeof(Window3x3)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .ToDictionary(i => i.Name, i => (double[])i.GetValue(null)!);

        private readonly Dictionary<string, string> fileToPath = [];

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) =>
            InitializeImage(fileToPath[Files[SelectedFile]], Window3x3.Laplacian8);

        private void ComboBoxWindow_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) =>
            InitializeImage(fileToPath[Files[SelectedFile]], windowDict[Windows[SelectedWindow]]);
    }
}