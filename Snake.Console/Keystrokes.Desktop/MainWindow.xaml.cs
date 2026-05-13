using System.IO;
using System.Text;
using System.Windows;

using Keystrokes.Core;
using Keystrokes.Core.Models;
using Keystrokes.Core.Services;

namespace Keystrokes.Desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private MainViewModel vm;

    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = vm = new MainViewModel();

        var files = Directory.EnumerateFiles("../../../Keystrokes");

        var maybeSamples = files
            .Select(Sample.FromFile)
            .Flatten()
            ;

        if (maybeSamples.IsError)
            return;

        var samples = maybeSamples.Value.ToArray();

        var actualUserId = samples.Select(i => i.UserId).ToArray();

        (string Name, DistanceCallback Distance)[] distances = [
            ("Euclidean", Distances.Euclidean),
            ("Chebyshev", Distances.Chebyshev),
            ("Manhattan", Distances.Manhattan),
        ];

        var sb = new StringBuilder();

        for (int k = 1; k < 5; k++)
            foreach ((string Name, DistanceCallback Distance) in distances)
            {
                var predictedUserId = Classifiers.KNN(samples, Distance, k).ToArray();

                double accuracy = Math.Round(
                    actualUserId.Zip(
                        predictedUserId,
                        (i, j) => i == j ? 1.0 : 0.0
                    ).Sum() / predictedUserId.Length * 100, 2
                );

                sb.AppendLine($"Distance: {Name}, K: {k}, Accuracy: {accuracy}");
            }

        vm.MainText = sb.ToString();
    }
}