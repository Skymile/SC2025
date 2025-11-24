using System.Globalization;

using Keystrokes.Console;

const string inputDir = "../../../Keystrokes";

var samples = FileHandler
    .FromDirectory(inputDir)
    .ToArray();

var kValues = Enumerable
    .Range(1, 10)
    .Select(i => i * 2 - 1)
    .ToArray();

(string Name, Distance Metric)[] metrics = [
        (nameof(DistanceMetrics.Euclidean ), DistanceMetrics.Euclidean ),
        (nameof(DistanceMetrics.Manhattan ), DistanceMetrics.Manhattan ),
        (nameof(DistanceMetrics.Chebyshev ), DistanceMetrics.Chebyshev ),
        (nameof(DistanceMetrics.Canberra  ), DistanceMetrics.Canberra  ),
        (nameof(DistanceMetrics.Sum       ), DistanceMetrics.Sum       ),
        (nameof(DistanceMetrics.Min       ), DistanceMetrics.Min       ),
        (nameof(DistanceMetrics.BrayCurtis), DistanceMetrics.BrayCurtis)
];

Console.WriteLine(string.Join(
    Environment.NewLine,
    from metric in metrics
    from k in kValues
    let knn = Classifiers.KNN(k)
    select Display.ComputeAccuracy(
        $"{k} - {metric.Name}: ", samples, metric.Metric, knn
    )
));

namespace Keystrokes.Console
{
    public record Keystroke(
        string Key, 
        double DwellTime, 
        double FlightTime
    )
    {
        private static bool Parse(string value, out double parsed) =>
            double.TryParse(
                value,
                CultureInfo.InvariantCulture,
                out parsed
            );

        public static Keystroke? FromArray(string[] data) =>
            data.Length == 3 &&
            Parse(data[1], out var dwellTime) &&
            Parse(data[2], out var flightTime)
                ? new Keystroke(data[0], dwellTime, flightTime)
                : null;

        public static Keystroke? FromLine(string line) =>
            line.Split(',',
                StringSplitOptions.RemoveEmptyEntries |
                StringSplitOptions.TrimEntries
            ) is string[] data and { Length: 3 }
                ? FromArray(data)
                : null;
    }

    public record Sample(
            string UserId, 
            Keystroke[] Keystrokes
        ) 
    {
        public double[] DwellTimes { get; } =
            [.. Keystrokes.Select(i => i.DwellTime)];

        public double[] FlightTimes { get; } =
            [.. Keystrokes.Select(i => i.FlightTime)];
    }

    public static class FileHandler
    {
        public static Sample? FromFile(string filePath) =>
            !string.IsNullOrWhiteSpace(filePath) &&
            File.Exists(filePath) &&
            Path.GetFileNameWithoutExtension(filePath)
                .Split('_')
                is string[] split &&
            split.Length == 2
                ? new Sample(
                    split[0], 
                    [.. File.ReadLines(filePath)
                            .Select(Keystroke.FromLine)
                            .Where(i => i is not null)!]
                )
                : null;

        public static IEnumerable<Sample> FromDirectory(string directory) =>
            from file in Directory.EnumerateFiles(directory)
            let sample = FromFile(file)
            where sample is not null
            select sample;
    }

    public delegate double Distance(
        double[] first, 
        double[] second
    );

    public static class DistanceMetrics
    {
        public static readonly Distance Euclidean = (f, s) =>
            Math.Sqrt(f.Zip(s, (i, j) => i - j).Sum(i => i * i));

        public static readonly Distance Manhattan = (f, s) =>
            f.Zip(s, (i, j) => Math.Abs(i - j)).Sum();

        public static readonly Distance Chebyshev = (f, s) =>
            f.Zip(s, (i, j) => Math.Abs(i - j)).Max();

        public static readonly Distance Canberra = (f, s) =>
            f.Zip(s, (i, j) => Math.Abs(i - j) / (i + j)).Sum();

        public static readonly Distance Sum = (f, s) =>
            f.Zip(s, (i, j) => i + j).Sum();

        public static readonly Distance Min = (f, s) =>
            f.Zip(s, (i, j) => Math.Min(i, j)).Sum();

        public static readonly Distance BrayCurtis = (f, s) =>
            1.0 - 2.0 * Min(f, s) / Sum(f, s);
    }

    public delegate string Classifier(
        Sample current,
        Sample[] training,
        Distance distance
    );

    public static class Classifiers
    {   
        public static Classifier KNN(int k) =>
            (current, training, distance) => (
               from j in (
                        from i in training
                        let dist = distance(
                            current.DwellTimes,
                            i.DwellTimes)
                        orderby dist
                        select (Distance: dist, Id: i.UserId)
                    ).Take(k)
               group j by j.Id into p
               orderby p.Count() descending
               select p.First()
            ).First().Id;
    }

    public static class Predictor
    {
        public static IEnumerable<bool> LeaveOneOut(
                Sample[] samples,
                Distance distance,
                Classifier classifier
            ) =>
            from sample in samples
            let training = samples
                .Where(i => i != sample)
                .ToArray()
            let acc = classifier(sample, training, distance)
            select acc == sample.UserId;
    }

    public static class Display
    {
        public static string ComputeAccuracy(
                string tag,
                Sample[] samples,
                Distance distance,
                Classifier classifier
            ) => string.Concat(
                tag.PadLeft(16, ' '),
                (Predictor.LeaveOneOut(samples, distance, classifier)
                    .Count(i => i) / (double)samples.Length
                ).ToString("0.00%").PadLeft(16, ' ')
            );
    }
}