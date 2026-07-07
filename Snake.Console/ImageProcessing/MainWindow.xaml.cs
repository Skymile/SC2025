using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageProcessing
{
    public class MainWindomVM : INotifyPropertyChanged
    {
        public MainWindomVM()
        {
            NiblackK = 8;
            NiblackP = 12;

            const string path = @"C:\Samples\Fingerprints";

            (string Filename, IReadOnlyDictionary<MinutiaeType, int> Minutaie)[] pictures = Directory
                .EnumerateFiles(path)
                .Select(Picture.Create)
                .Select(pic => pic
                    .Apply(Algorithms.ThresholdBinarization)
                    .Apply(Algorithms.K3M)
                    .GetMinutiate()
                )
                .ToArray();

            var sb = new StringBuilder();

            //foreach ((string Filename, IReadOnlyDictionary<MinutiaeType, int> Minutaie) in pictures)
            //{
            //    sb.AppendLine(Path.GetFileNameWithoutExtension(Filename));
            //    foreach (var m in Minutaie)
            //        sb.AppendLine("\t" + string.Join(": ", m.Key, m.Value));
            //}

            foreach (var group in pictures
                .GroupBy(i => 
                    (i.Minutaie[MinutiaeType.Island     ] / 100) * 1 +
                    (i.Minutaie[MinutiaeType.Ending     ] / 100) * 1_00 +
                    (i.Minutaie[MinutiaeType.Line       ] / 100) * 1_000 +
                    (i.Minutaie[MinutiaeType.Crossing   ] / 100) * 1_0000 +
                    (i.Minutaie[MinutiaeType.Bifurcation] / 100) * 1_00000
                ))
            {
                sb.AppendLine(group.Key.ToString());
                foreach (var m in group)
                    sb.AppendLine("\t" + m.Filename);
            }

            Output = sb.ToString();
        }

        public required BitmapSource ImageSource { get; set; }

        public string Output   { get; set => Set(ref field, value); }
        public double NiblackK { get; set => Set(ref field, value, Refresh); }
        public double NiblackP { get; set => Set(ref field, value, Refresh); }

        private void Refresh()
        {
            ImageSource = new Picture("C:/Samples/apple.png")
                .Apply(Algorithms.Dilation, new Size(3, 3))
                .Apply(Algorithms.Phansalkar(
                    NiblackK / 10.0,
                    NiblackP / 10.0
                ), new Size(3, 3))
                .ToBitmapSource();

            PropertyChanged?.Invoke(this, new(nameof(ImageSource)));
        }

        private void Set<T>(ref T field, T value, Action action, [CallerMemberName] string name = "")
        {
            field = value;
            PropertyChanged?.Invoke(this, new(name));
            action();
        }

        private void Set<T>(ref T field, T value, [CallerMemberName] string name = "")
        {
            field = value;
            PropertyChanged?.Invoke(this, new(name));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new MainWindomVM()
            {
                ImageSource = new Picture("C:/Samples/fingerprint.png")
                    //.Apply(Algorithms.Dilation, new Size(3, 3))
                    //.Apply(Algorithms.Dilation, new Size(3, 3))
                    //.Apply(Algorithms.Dilation, new Size(3, 3))
                    //.Apply(Algorithms.Emboss, new Size(3, 3))
                    //.Apply(Algorithms.Emboss, new Size(3, 3))
                    //.Apply(Algorithms.Mean)
                    //.Apply(Algorithms.Pixelization, new Size(12, 12))
                    .Apply(Algorithms.ZhangSuen)
                    //.Apply(Algorithms.ThresholdBinarization)
                    .ToBitmapSource()
            };
            this.DataContext = vm;
        }
    }
}