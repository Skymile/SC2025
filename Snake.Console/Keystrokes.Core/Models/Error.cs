namespace Keystrokes.Core.Models;

public static partial class Error
{
    public const string IdentifierNotFound = "Identifier not found in the filename!";
    public const string FileDoesNotExist = "The file does not exist";
    public const string TimesInDifferentFormat = "Times were not in a correct format!";

    public static Result<T> TokensNotThreeElements<T>(IEnumerable<string> tokens) => 
        Result.Create<T>(string.Format(
                string.Join(
                    Environment.NewLine,
                    "Tokens were not an array with 3 elements! ",
                    "The tokens were: {0}"
                ),
                tokens
            ));
}
