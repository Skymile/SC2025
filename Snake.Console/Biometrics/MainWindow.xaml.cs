using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

using Biometrics.Core;
using Biometrics.Extensions;
using Biometrics.Services;
using Biometrics.ViewModels;

namespace Biometrics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowVM vm, IAlgorithmService algoService)
        {
            InitializeComponent();
            this.algoService = algoService;
            
            this.Title = Localization.Instance.Title;
            this.DataContext = vm;
            vm.RefreshImage = UpdateAlgorithm;
            UpdateAlgorithm();
        }

        private void UpdateAlgorithm()
        {
            MainImg.Source = vm.GetImage().ToBitmapSource();

            if (string.IsNullOrWhiteSpace(vm.SelectedAlgorithm))
                return;

            var type = algoService.GetAlgorithmType(vm.SelectedAlgorithm);

            AlgorithmConfig.Children.Clear();
            foreach (var i in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                string name = i.Name;
                if (i.GetCustomAttribute<NameAttribute>() is NameAttribute attr)
                    name = attr.Name;

                AlgorithmConfig.Children.Add(new Label()
                {
                    Content = name
                });

                if (i.PropertyType == typeof(byte))
                {
                    AlgorithmConfig.Children.Add(new Slider() 
                    { 
                        Minimum = 0, 
                        Maximum = 255,
                        TickFrequency = 1,
                        TickPlacement = TickPlacement.BottomRight
                    });
                }
                else if (i.PropertyType == typeof(double))
                {
                    AlgorithmConfig.Children.Add(new Slider()
                    {
                        Minimum = 0.0,
                        Maximum = 2.0,
                        TickFrequency = 0.1,
                    });
                }
            }
        }

        private readonly IAlgorithmService algoService;
        private MainWindowVM vm => (MainWindowVM)DataContext;
    }
}