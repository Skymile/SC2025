namespace Keystrokes.Core.Models;

public record Keystroke(
    string Key,
    double DwellTime,
    double FlightTime
)
{
    public static Result<Keystroke> FromTokens(IEnumerable<string> tokens) => Result
        .Create(tokens)
        .Then(tokens => tokens.ToArray())
        .And(tokens => tokens.Length == 3, "")
        .And(t => double.TryParse(t[1], out _) , Error.TimesInDifferentFormat)
        .And(t => double.TryParse(t[2], out _), Error.TimesInDifferentFormat)
        .Then(t => new Keystroke(t[0], double.Parse(t[1]), double.Parse(t[2])));

    public static Result<Keystroke> FromLine(string? line) => Result
        .Create(line)
        .And(line => !string.IsNullOrWhiteSpace(line), "Line was empty or whitespace!")
        .Then(line => FromTokens(
            from token in line!.Trim().Split(',')
            select token.Trim()
        ))
        .Collapse();
}
