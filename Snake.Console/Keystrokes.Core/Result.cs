namespace Keystrokes.Core;

public static class Result
{
    public static Result<T> Create<T>() => new("Not initialized");
    public static Result<T> Create<T>(T value) => new(value);
    public static Result<T> Create<T>(string error) => new(error);

    public static Result<IEnumerable<T>> Flatten<T>(
        this IEnumerable<Result<T>> data) =>

        data.Any(i => i.IsError)

            ? Create<IEnumerable<T>>(string.Join(Environment.NewLine,
                from result in data
                where result.IsError
                select result.Error
            ))

            : Create(data.Select(i => i.Value));

    public static Result<T> Collapse<T>(this Result<Result<T>> result) =>
        result.IsSuccess && result.Value.IsSuccess
            ? new Result<T>(result.Value.Value)
            : result.IsError
                ? new Result<T>(result.Error)
                : result.Value;
}

public class Result<T>
{
    public Result(T value)
    {
        this.Value = value;
        this.IsSuccess = true;
        this.Error = string.Empty;
    }

    public Result(string error)
    {
        this.Value = default!;
        this.Error = error;
        this.IsError = true;
    }

    public Result<U> Then<U>(Func<T, U> converter) =>
        IsSuccess 
            ? new(converter(Value)) 
            : new Result<U>(Error);

    public Result<T> And(Func<T, bool> condition, string error) =>
        !condition(Value) ? new(error) : this;

    public Result<U> Then<U>(Func<T, bool> condition, Func<T, U> converter, string error) =>
        IsSuccess && condition(Value)
            ? new(converter(Value))
            : new Result<U>(IsError ? Error : error);

    public readonly T Value;
    public readonly string Error;
    public readonly bool IsError;
    public readonly bool IsSuccess;
}
