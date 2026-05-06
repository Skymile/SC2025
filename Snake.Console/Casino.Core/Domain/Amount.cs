namespace Casino.Domain;

public record Amount(decimal Value)
{
    public const int MaxAmount = 1_000_000;
    public const int MinAmount = 100;

    public override string ToString() => Value.ToString();

    public static Result<Amount> TryCreate(decimal value) =>
        value < MinAmount || value > MaxAmount
            ? Result.Create<Amount>(
                $"The value must be between greater than {MinAmount} and less than {MaxAmount:# ### ### ###}")
            : Result.Create(new Amount(value));

    public static Amount operator +(Amount left, Amount right) =>
        new(left.Value + right.Value);
    public static Amount operator -(Amount left, Amount right) =>
        new(left.Value - right.Value);
}
