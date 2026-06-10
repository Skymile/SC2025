using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        }

        public required BitmapSource ImageSource { get; set; }

        public double NiblackK 
        { 
            get => field;
            set
            {
                Set(ref field, value);
                Refresh();
            }
        }

        public double NiblackP
        {
            get => field;
            set
            {
                Set(ref field, value);
                Refresh();
            }
        }

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
                ImageSource = new Picture("C:/Samples/apple.png")
                    .Apply(Algorithms.Dilation, new Size(3, 3))
                    .Apply(Algorithms.Dilation, new Size(3, 3))
                    .Apply(Algorithms.Dilation, new Size(3, 3))
                    //.Apply(Algorithms.Emboss, new Size(3, 3))
                    //.Apply(Algorithms.Emboss, new Size(3, 3))
                    //.Apply(Algorithms.Mean)
                    .Apply(Algorithms.Pixelization, new Size(12, 12))
                    //.Apply(Algorithms.ThresholdBinarization)
                    .ToBitmapSource()
            };
            this.DataContext = vm;
        }
    }
}