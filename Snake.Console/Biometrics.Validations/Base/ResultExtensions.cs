namespace Biometrics.Validations.Base;

public static class ResultExtensions 
{
    public static Result<T, TErr> MergeErrors<T, TErr>(
            this Result<T, TErr[]> result,
            Func<TErr[], TErr> merger
        ) =>
        result.IsFailure
            ? Result<T, TErr>.Failure(merger(result.Error!))
            : Result<T, TErr>.Ok(result.Value);
}
