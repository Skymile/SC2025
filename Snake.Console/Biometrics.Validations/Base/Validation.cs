namespace Biometrics.Validations.Base;

public static class Validation
{
    public static Validation<T> Validate<T>(
        string paramName,
        Func<T> getValue,
        params object?[] errors
    )
    {
        var validationErrors =
            errors
            .OfType<object>()
            .Select(error => error is string str ? new Error(paramName, str) : (Error)error!)
            .ToArray();

        return validationErrors.Length > 0
            ? Validation<T>.Failure(validationErrors)
            : Validation<T>.Ok(getValue());
    }
}

public record Validation<T>(
    T Value,
    Error[] Error,
    bool IsSuccess,
    bool IsFailure)
{
    public static Validation<T> Ok(T value) =>
        new(value, default!, true, false);

    public static Validation<T> Failure(params Error[] errors) =>
        new(default!, errors, false, true);
}
