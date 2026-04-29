namespace Casino;

public static class Result
{
    public static Result<T> Create<T>(T value) => new(value);
    public static Result<T> Create<T>(string error) => new(error);
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

    public readonly T Value;
    public readonly string Error;
    public readonly bool IsError;
    public readonly bool IsSuccess;
}