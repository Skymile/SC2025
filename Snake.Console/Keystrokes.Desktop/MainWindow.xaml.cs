using System.Diagnostics;
using System.Diagnostics.Contracts;
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

        var classificationStrategy = new ClassifierStrategy()
        {
            Classifier = new KNearestNeighbours(Distances.Euclidean, 3)
        };
        var sb = new StringBuilder();

        for (int k = 1; k < 5; k++)
            foreach ((string Name, DistanceCallback Distance) in distances)
            {
                classificationStrategy.Classifier = new KNearestNeighbours(Distance, k);
                var predictedUserId = classificationStrategy.Apply(samples).ToArray();

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

    private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        swDwell ??= Stopwatch.StartNew();
        swDwell = Stopwatch.StartNew();
        currentKeystroke = new Keystroke(e.Key.ToString(), 
            swDwell.ElapsedMilliseconds, 
            swFlight?.ElapsedMilliseconds ?? -1
        );
        swFlight = Stopwatch.StartNew();

        if (currentKeystroke?.FlightTime > 0)
        {
            userKeystrokes.Add(new(
                e.Key.ToString(),
                swDwell!.ElapsedMilliseconds,
                swFlight!.ElapsedMilliseconds
            ));

            Title = userKeystrokes.Count.ToString();



            var files = Directory.EnumerateFiles("../../../Keystrokes");

            var maybeSamples = files
                .Select(Sample.FromFile)
                .Flatten()
                ;

            if (maybeSamples.IsError)
                return;

            var samples = maybeSamples.Value.ToArray();

            int k = 3;
            DistanceCallback distance = Distances.Manhattan;
            KNearestNeighbours classifier = new KNearestNeighbours(distance, k);
            var predictedUserId = classifier.Apply(new Sample(99, 99, userKeystrokes.ToArray()), samples);
            vm.MainText = predictedUserId.ToString();
        }
    }

    private void TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
    {
        swDwell ??= Stopwatch.StartNew();
        swFlight ??= Stopwatch.StartNew();

        currentKeystroke = new Keystroke(e.Key.ToString(), swDwell.ElapsedMilliseconds, -1.0);

        swFlight = Stopwatch.StartNew();
    }

    private readonly List<Keystroke> userKeystrokes = [];

    private Keystroke? currentKeystroke;
    private Stopwatch? swDwell;
    private Stopwatch? swFlight;
}