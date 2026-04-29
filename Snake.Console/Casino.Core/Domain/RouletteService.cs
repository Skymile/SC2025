namespace Casino.Domain;

public class RouletteService(RouletteAggregate Roulette)
{
    public Pocket GetRandomRoulette(int seed = -1)
    {
        var random = seed == -1 ? new Random() : new Random(seed);

        return Roulette.Pockets[
            random.Next(0, Roulette.Pockets.GetLength(0)),
            random.Next(0, Roulette.Pockets.GetLength(1))
        ];
    }

    public bool IsDoubleStreet(
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
        TryGetRight(bottomLeft) == bottomRight;

    public bool IsEdge(Pocket index1, Pocket index2)
    {
        if (index1 == index2)
            return false;
        int rows = RouletteAggregate.RowLength;
        int cols = RouletteAggregate.ColumnLength;

        for (int row = 0; row < rows; row++)
            for (int col = 0; col < cols; col++)
            {
                if (Roulette.Pockets[row, col] != index1)
                    continue;

                if (TryGetLeft(index1) == index2 ||
                    TryGetRight(index1) == index2 ||
                    TryGetUp(index1) == index2 ||
                    TryGetDown(index1) == index2)
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
            ? Roulette.Pockets[
                pocket.RowIndex + rowOffset,
                pocket.ColumnIndex + columnOffset]
            : null;

    public bool IsInRange(int column, int row) =>
        column >= 0 &&
        column < 3 &&
        row >= 0 &&
        row < 12;
}
