using System.Drawing;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

using Biometrics.Core;
using Biometrics.Core.Algorithms;
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

            Dictionary<string, Func<string>> parameters = new();

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

                    var txtBox = new TextBox();
                    var label = new Label();
                    
                    if (type == typeof(PhansalkarBinarization))
                        parameters[i.Name] = () => txtBox.Text;

                    label.Content = "Test";
                    label.Foreground = System.Windows.Media.Brushes.Red;

                    // We need to unsubscribe -=
                    txtBox.TextChanged += (s, e) =>
                    {
                        if (type == typeof(PhansalkarBinarization) &&
                            parameters.TryGetValue(i.Name, out var getter))
                        {
                            var res = PhansalkarBinarization.TryCreate(
                                parameters.TryGetValue("K", out var funcK) ? funcK() : "",
                                parameters.TryGetValue("P", out var funcP) ? funcP() : "",
                                parameters.TryGetValue("Q", out var funcQ) ? funcQ() : ""
                            );

                            if (res.IsSuccess)
                                label.Content = "Ok";
                            else
                            {
                                var err2 = res
                                    .Error
                                    .Where(j => j.ParamName.ToUpperInvariant() == i.Name.ToUpperInvariant())
                                    .ToArray();

                                var err = res
                                    .Error
                                    .Where(j => j.ParamName.ToUpperInvariant() == i.Name.ToUpperInvariant())
                                    .Select(j => j.Message)
                                    .ToArray();

                                label.Content = string.Join(", ", err);
                            }

                            label.Visibility = Visibility.Visible;
                        }
                    };

                    AlgorithmConfig.Children.Add(txtBox);
                    AlgorithmConfig.Children.Add(label);
                }
            }
        }

        private readonly IAlgorithmService algoService;
        private MainWindowVM vm => (MainWindowVM)DataContext;
    }
}