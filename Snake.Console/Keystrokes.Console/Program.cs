
using System.Globalization;

const string inputDir = "../../../Keystrokes";

Console.WriteLine(
    Directory.EnumerateFiles(inputDir).Count()
);

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
            string.IsNullOrWhiteSpace(filePath) &&
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
    }
}