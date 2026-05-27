using Casino.Services;

namespace Casino.Domain;

public record Amount(decimal Value)
{
    public const int MaxAmount = 1_000_000;
    public const int MinAmount = 100;

    public override string ToString() => Value.ToString();

    public static Result<Amount> TryCreate(decimal value) =>
        Result.If(
            value >= MinAmount && value <= MaxAmount,
            new Amount(value),
            Text.ValueMustBeGreater.Format(MinAmount, MaxAmount)
        );

    public static Amount operator +(Amount left, Amount right) =>
        new(left.Value + right.Value);
    public static Amount operator -(Amount left, Amount right) =>
        new(left.Value - right.Value);
}
