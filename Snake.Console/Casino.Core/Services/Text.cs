namespace Casino.Services;

public static class Text
{
    public static string Format(this string format, params object[] args) =>
        string.Format(format, args);

    public const string ValueMustBeBetween =
        "The value must be between {0} and {1}";
    public const string TypeAmount =
        "Type the amount between {0} and {1:# ### ###}.";
    public const string TypePocketNumber =
        "Type the pocket number between {0} and {1}.";
}
