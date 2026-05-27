using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageProcessing
{
    public class MainWindomVM
    {
        public required BitmapSource ImageSource { get; set; }
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
                    .Apply(Algorithms.Emboss, new Size(3, 3))
                    .Apply(Algorithms.Emboss, new Size(3, 3))
                    //.Apply(Algorithms.Mean)
                    //.Apply(Algorithms.ThresholdBinarization)
                    .ToBitmapSource()
            };
            this.DataContext = vm;
        }
    }
}