namespace Biometrics.Validations.Base;

public record Result<T, TErr>(
    T Value,
    TErr Error,
    bool IsSuccess,
    bool IsFailure)
{
    public static Result<T, TErr> Ok(T value) =>
        new(value, default!, true, false);

    public static Result<T, TErr> Failure(TErr error) =>
        new(default!, error, false, true);

    public static Result<T, TErr[]> Validation(
            Func<T> getValue,
            params TErr?[] errors
        )
    {
        errors = errors.OfType<TErr>().ToArray();
        return 
            errors.Length > 0
                ? Result<T, TErr[]>.Failure(errors!)
                : Result<T, TErr[]>.Ok(getValue());
    }
}
