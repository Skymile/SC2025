using Casino.Services;

namespace Casino.Domain;

/// <summary>
/// A single spot in a roulette
/// </summary>
public class Pocket(int Value, Color Color)
{
    public int RowIndex => (Value - 1) / 3;
    public int ColumnIndex => (Value - 1) % 3;

    public int Value { get; } = Value;
    public Color Color { get; } = Color;

    public const int MaxPocket = 36;
    public const int MinPocket = 0;

    public static Result<Pocket> TryCreate(int value) =>
        value < MinPocket || value >= MaxPocket
            ? Result.Create<Pocket>(
                Text.ValueMustBeBetween.Format(MinPocket, MaxPocket)
            )
            : Result.Create(new Pocket(
                value,
                value == 0             ? Color.Green : 
                redSet.Contains(value) ? Color.Red : Color.Black
            ));

    public static bool operator==(Pocket? left, Pocket? right) =>
        left?.Value == right?.Value;

    public static bool operator!=(Pocket? left, Pocket? right) =>
        left?.Value != right?.Value;

    public override bool Equals(object? o) =>
        o is Pocket p && p.Value == this.Value;

    private static readonly HashSet<int> redSet = 
        [1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36];

    public override int GetHashCode() => Value;
}
