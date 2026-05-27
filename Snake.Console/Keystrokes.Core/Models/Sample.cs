namespace Keystrokes.Core.Models;

public record Sample(
    int SampleId,
    int UserId,
    Keystroke[] Keystrokes
)
{
    public double[] DwellTimes  => [.. Keystrokes.Select(i => i.DwellTime)];
    public double[] FlightTimes => [.. Keystrokes.Select(i => i.FlightTime)];

    public static Result<Sample> FromFile(string path) => Result
        .Create(path)
        .And(File.Exists, Error.FileDoesNotExist)
        .Then(v => File
            .ReadLines(v)
            .Where(i => !string.IsNullOrWhiteSpace(i))
            .Select(Keystroke.FromLine)
            .Flatten()
        )
        .Collapse()
        .Then(keystrokes => 
            (Filename: Path.GetFileNameWithoutExtension(path)[1..3], Keystrokes: keystrokes))
        .And(r => int.TryParse(r.Filename, out int id), Error.IdentifierNotFound)
        .Then(r => new Sample(++count, int.Parse(r.Filename), [.. r.Keystrokes]));

    private static int count = 0;
}
