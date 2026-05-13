namespace Keystrokes.Core.Models;

public record Keystroke(
    string Key,
    double DwellTime,
    double FlightTime
)
{
    public static Result<Keystroke> FromTokens(IEnumerable<string> tokens) =>
        tokens.ToArray() is { Length: 3 } arr
            ? double.TryParse(arr[1], out var dwellTime) &&
              double.TryParse(arr[2], out var flightTime)
                ? Result.Create(new Keystroke(arr[0], dwellTime, flightTime))
                : Error.TimesInDifferentFormat<Keystroke>()
            : Error.TokensNotThreeElements<Keystroke>(tokens);

    public static Result<Keystroke> FromLine(string? line) =>
        string.IsNullOrWhiteSpace(line)
            ? Result.Create<Keystroke>("Line was empty or whitespace!")
            : FromTokens(
                from token in line.Trim().Split(',')
                select token.Trim()
            );
}
