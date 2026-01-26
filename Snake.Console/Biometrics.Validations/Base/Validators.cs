namespace Biometrics.Validations.Base;

public static class Validators
{
    public static string MergeStrings(string[] strings) =>
        string.Join(Environment.NewLine, strings);

    public static Error MergeErrors(Error[] errors) =>
        errors.Length > 0
            ? new(errors[0].ParamName,
                  MergeStrings([.. errors.Select(i => i.Message)]))
            : throw new InvalidOperationException();
}
