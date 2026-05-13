namespace Keystrokes.Core.Models;

public static class Error
{
    public static Result<T> TokensNotThreeElements<T>(IEnumerable<string> tokens) => 
        Result.Create<T>(string.Format(
                string.Join(
                    Environment.NewLine,
                    "Tokens were not an array with 3 elements! ",
                    "The tokens were: {0}"
                ),
                tokens
            ));

    public static Result<T> TimesInDifferentFormat<T>() =>
        Result.Create<T>("Times were not in a correct format!");
}
