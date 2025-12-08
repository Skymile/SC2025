using System.Drawing;
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

            var bmp = Image.Apply(new Bitmap("../../../Samples/apple.png"))
                
                ;
            MainImg.Source = bmp.ToBitmapSource();
        }
    }
}