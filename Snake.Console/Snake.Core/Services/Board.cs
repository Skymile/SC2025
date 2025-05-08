
public class Board(Config cfg)
{
    public int MapWidth  { get; private set; } = cfg.MapWidth;
    public int MapHeight { get; private set; } = cfg.MapHeight;

    public IEnumerable<Point> GetRandomPositions(
        IEnumerable<Point> taken, int count)
        {
            var dict = taken
                .GroupBy(i => i.X)
                .ToDictionary(i => i.Key, i => i.Select(i => i.Y).ToHashSet());

            List<Point> freeCells = [];
            for (int x = 0; x < MapWidth; x++)
                for (int y = 0; y < MapHeight; y++)
                {
                    if (dict.TryGetValue(x, out var takenY) &&
                        takenY.Contains(y))
                        continue;

                    freeCells.Add(new(x, y));
                }

            for (int r = 0; r < count; r++)
            {
                int number = Random.Shared.Next(0, freeCells.Count);
                var found = freeCells[number];
                freeCells.RemoveAt(number);
                yield return found;
            }
        }
}