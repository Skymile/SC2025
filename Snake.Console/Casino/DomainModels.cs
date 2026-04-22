namespace Casino;

// Event
public record BetPlaced(Player player, Bet bet);
public record BetLost(Player player, Bet bet);
public record BetWon(Player player, Bet bet);
public record RouletteSpun();

// Aggregate Root
/// <summary>
/// A roulette containing pockets
/// </summary>
public record RouletteAggregate(Pocket[,] Pockets)
{
    public void Spin(RouletteSpun data)
    {

    }

    public void PlaceBet(BetPlaced data)
    {

    }

    public static Result<RouletteAggregate> TryCreate()
    {
        var r = new RouletteAggregate(new Pocket[RowLength, ColumnLength]);

        for (int row = 0; row < RowLength; row++)
            for (int col = 0; col < ColumnLength; col++)
                r.Pockets[row, col] = 
                    Pocket.TryCreate(row * ColumnLength + col + 1).Value;

        return Result.Create<RouletteAggregate>(r);
    }

    public bool IsDoubleStreet(Pocket[,] table,
        Pocket topLeft, Pocket topMiddle, Pocket topRight,
        Pocket bottomLeft, Pocket bottomMiddle, Pocket bottomRight
        ) =>
        IsStreet(topLeft, topMiddle, topRight) &&
        IsStreet(bottomLeft, bottomMiddle, bottomRight) &&
        TryGetDown(topMiddle) == bottomMiddle
        ;

    public bool IsStreet(Pocket left, Pocket middle, Pocket right) =>
        TryGetRight(left) == middle &&
        TryGetRight(middle) == right;

    public bool IsCorner(Pocket topLeft, Pocket topRight, Pocket bottomLeft, Pocket bottomRight) =>
        TryGetRight(topLeft) == topRight &&
        TryGetDown(topLeft) == bottomLeft &&
        TryGetRight(bottomLeft) == bottomRight ;

    public bool IsEdge(Pocket index1, Pocket index2)
    {
        if (index1 == index2)
            return false;
        int rows = RowLength;
        int cols = ColumnLength;

        for (int row = 0; row < rows; row++)
            for (int col = 0; col < cols; col++)
            {
                if (Pockets[row, col] != index1)
                    continue;

                if (TryGetLeft (index1) == index2 ||
                    TryGetRight(index1) == index2 ||
                    TryGetUp   (index1) == index2 ||
                    TryGetDown (index1) == index2 )
                    return true;
            }
        return false;
    }

    public Pocket? TryGetLeft(Pocket pocket) => TryGetNeighbor(pocket, -1, 0);
    public Pocket? TryGetRight(Pocket pocket) => TryGetNeighbor(pocket, 1, 0);
    public Pocket? TryGetUp(Pocket pocket) => TryGetNeighbor(pocket, 0, -1);
    public Pocket? TryGetDown(Pocket pocket) => TryGetNeighbor(pocket, 0, 1);

    public Pocket? TryGetNeighbor(
        Pocket pocket, int columnOffset, int rowOffset) =>
        IsInRange(
            pocket.ColumnIndex + columnOffset, 
            pocket.RowIndex + rowOffset
        )
            ? Pockets[
                pocket.RowIndex + rowOffset,
                pocket.ColumnIndex + columnOffset]
            : null;

    public bool IsInRange(int column, int row) =>
        column >= 0 && 
        column < 3 &&
        row >= 0 && 
        row < 12;

    private const int RowLength = 12;
    private const int ColumnLength = 3;
}

// Value Objects
/// <summary>
/// All possible color values for a single pocket
/// </summary>
public enum Color
{
    Green,
    Black,
    Red
}

/// <summary>
/// A single spot in a roulette
/// </summary>
public class Pocket(int Value, Color Color)
{
    public int RowIndex => (Value - 1) / 3;
    public int ColumnIndex => (Value - 1) % 3;

    public int Value { get; } = Value;
    public Color Color { get; } = Color;

    public static Result<Pocket> TryCreate(int value) =>
        value < 0 && value >= 37
            ? Result.Create<Pocket>("The value must be between 0 and 37")
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


// Entities

public record Bet(Guid Id, decimal Amount);

public record Player(Guid Id);
