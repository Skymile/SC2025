
const int Size = 37;

var pockets = Pockets.TryCreate()
    ?? throw new ArgumentException();
var colors = PocketColors.TryCreate()
    ?? throw new ArgumentException();

int index = 1;
var fgColor = Console.ForegroundColor;
var bgColor = Console.BackgroundColor;
Console.WriteLine("      0");

while (index < Size)
{
    (Console.ForegroundColor, Console.BackgroundColor) =
        colors[index] == Color.Black
            ? (ConsoleColor.White, ConsoleColor.Black)
            : (ConsoleColor.Black, ConsoleColor.White);
    
    Console.Write($"{index,3} ");
    int row = index / 3;
    int col = index % 3;

    pockets.Table[
        (index - 1) / 3,
        (index - 1) % 3
    ] = Pocket.TryCreate(index) 
        ?? throw new ArgumentException($"Value is not in range");

    if (index != 0 && col == 0)
        Console.WriteLine();

    ++index;
}
Console.ForegroundColor = fgColor;
Console.BackgroundColor = bgColor;

Console.WriteLine();
Console.WriteLine();

//Console.WriteLine("Type");
//var key = Console.ReadKey();

static bool IsCorner(Pocket[,] table, Pocket index1, Pocket index2, Pocket index3, Pocket index4)
{
    var set = new HashSet<Pocket>() { index1, index2, index3, index4 };
    int rows = table.GetLength(0);
    for (int row = 0; row < rows - 1; row++)
    {
        if (set.Contains(table[row + 0, 0]) &&
            set.Contains(table[row + 0, 1]) &&
            set.Contains(table[row + 1, 0]) &&
            set.Contains(table[row + 1, 1])
            )
            return true;

        if (set.Contains(table[row + 0, 1]) &&
            set.Contains(table[row + 0, 2]) &&
            set.Contains(table[row + 1, 1]) &&
            set.Contains(table[row + 1, 2])
            )
            return true;
    }
    return false;
}

static bool IsDoubleStreet(Pocket[,] table,
    Pocket index1, Pocket index2, Pocket index3,
    Pocket index4, Pocket index5, Pocket index6
    ) =>
    IsStreet(table, index1, index2, index3) &&
    IsStreet(table, index4, index5, index6) &&
    (
        IsEdge(table, index1, index4) ||
        IsEdge(table, index1, index5) ||
        IsEdge(table, index1, index6) ||
        IsEdge(table, index2, index4) ||
        IsEdge(table, index2, index5) ||
        IsEdge(table, index2, index6) ||
        IsEdge(table, index3, index4) ||
        IsEdge(table, index3, index5) ||
        IsEdge(table, index3, index6)
    );

static bool IsStreet(Pocket[,] table, Pocket index1, Pocket index2, Pocket index3)
{
    var set = new HashSet<Pocket>() { index1, index2, index3 };
    int rows = table.GetLength(0);
    for (int row = 0; row < rows; row++)
    {
        if (set.Contains(table[row, 0]) &&
            set.Contains(table[row, 1]) &&
            set.Contains(table[row, 2]))
            return true;
    }
    return false;
}

static bool IsEdge(Pocket[,] table, Pocket index1, Pocket index2)
{
    if (index1 == index2) 
        return false;
    int rows = table.GetLength(0);
    int cols = table.GetLength(1);
    for (int row = 0; row < rows; row++)
        for (int col = 0; col < cols; col++)
        {
            if (table[row, col] != index1)
                continue;

            if (col % rows != 0 && 
                col > 1 && 
                table[row, col - 1] == index2)
                return true;

            if (row % cols != 0 && 
                row > 1 && 
                table[row - 1, col] == index2)
                return true;

            if ((col == 0 || col % rows != 0) && 
                col + 1 < cols && 
                table[row, col + 1] == index2)
                return true;

            if ((row == 0 || row % cols != 0) && 
                row + 1 < rows && 
                table[row + 1, col] == index2)
                return true;
        }
    return false;
}

for (int i = 0; i < 30; i++)
{
    int x = i;
    int y = i + Random.Shared.Next(0, 4);

    while (x == y)
        y = i + Random.Shared.Next(0, 4);
    
    Pocket p1 = Pocket.TryCreate(x) ?? throw new ArgumentException();
    Pocket p2 = Pocket.TryCreate(y) ?? throw new ArgumentException();

    Console.WriteLine($"{x}, {y}, IsEdge: {IsEdge(pockets.Table, p1, p2)}");
}
return;

static int GetRouletteNumber()
{
    int result = -1;
    do
    {
        Console.WriteLine("Type a number between 0 and 36");

        if (!int.TryParse(Console.ReadLine(), out result) 
            || result < 0 
            || result >= 37)
            result = -1;

    } while (result == -1);
    return result;
}

static (int, Color) GetRandomRoulette(Color[] pockets)
{
    int number = Random.Shared.Next(0, pockets.Length);
    Color random = pockets[number];
    return (number, random);
}

var option = RouletteOption.Straight;
switch (option)
{
    case RouletteOption.Straight:
        int number = GetRouletteNumber();
        (int randomNumber, Color randomColor) = GetRandomRoulette(pockets.Table);

        Console.WriteLine($"Roulette: {randomNumber}: {randomColor}");
        Console.WriteLine(number == randomNumber
            ? "You win!" : "You lose!");
        break;

    case RouletteOption.Split:
        break;
    case RouletteOption.Street:
        break;
    case RouletteOption.Corner:
        break;
    case RouletteOption.SixLine:
        break;
}

public record struct PocketColors(Color[] Colors)
{
    public static PocketColors? TryCreate()
    {
        var colors = new Color[Size];
        colors[0] = Color.Green;
        for (int i = 1; i < Size; i++)
            colors[i] =
                i <= 10 || (i >= 19 && i <= 28)
                    ? i % 2 == 0
                        ? Color.Black : Color.Red
                    : i % 2 == 0
                        ? Color.Red : Color.Black;

        return new PocketColors(colors);
    }

    public Color this[int index] => Colors[index];

    private const int Size = 37;
}

// Value Object
public record struct Pockets(Pocket[,] Table)
{
    public static Pockets? TryCreate() =>
        new(new Pocket[12, 3]);
}

// Value Object
public record struct Pocket(int Value)
{
    public static Pocket? TryCreate(int value) =>
        value < 0 && value >= 37 
            ? null 
            : new Pocket(value);
}

//public override bool Equals([NotNullWhen(true)] object? obj) => 
//    obj is Pocket p && p.Value == Value;
//public static bool operator ==(Pocket left, Pocket right) => 
//    left.Value == right.Value;
//public static bool operator!=(Pocket left, Pocket right) =>
//    !(left == right);
//public override int GetHashCode() => Value;
