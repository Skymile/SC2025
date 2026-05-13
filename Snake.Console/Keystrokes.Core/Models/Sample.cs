namespace Keystrokes.Core.Models;

public record Sample(
    int SampleId,
    int UserId,
    Keystroke[] Keystrokes
)
{
    public double[] DwellTimes  => [.. Keystrokes.Select(i => i.DwellTime)];
    public double[] FlightTimes => [.. Keystrokes.Select(i => i.FlightTime)];

    public static Result<Sample> FromFile(string path) =>
        !File.Exists(path)
            ? Result.Create<Sample>("The file does not exist")
            : File.ReadLines(path)
                  .Where(i => !string.IsNullOrWhiteSpace(i))
                  .Select(Keystroke.FromLine)
                  .Flatten()
              is Result<IEnumerable<Keystroke>> result
                ?
                    result.IsSuccess
                    ? int.TryParse(Path.GetFileNameWithoutExtension(path)[1..3], out var id)
                        ? Result.Create(new Sample(++count, id, [.. result.Value]))
                        : Result.Create<Sample>("Identifier not found in the filename!")
                    : Result.Create<Sample>(result.Error)
                : Result.Create<Sample>("An unexpected error occurred while processing the file!");

    private static int count = 0;
}
