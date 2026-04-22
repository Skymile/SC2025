using Casino;

const int Size = 37;

var roulette = RouletteAggregate.TryCreate().Value;

int index = 1;
var fgColor = Console.ForegroundColor;
var bgColor = Console.BackgroundColor;
Console.WriteLine("      0");

for (int row = 0; row < 12; row++)
{
    for (int col = 0; col < 3; col++)
    {
        var pocket = roulette.Pockets[row, col];
        (Console.ForegroundColor, Console.BackgroundColor) =
            pocket.Color == Color.Black
                ? (ConsoleColor.White, ConsoleColor.Black)
                : (ConsoleColor.Black, ConsoleColor.White);
        Console.Write($"{pocket.Value,3} ");
    }
    Console.WriteLine();
}

Console.ForegroundColor = fgColor;
Console.BackgroundColor = bgColor;

Console.WriteLine();
Console.WriteLine();

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

static (int, Color) GetRandomRoulette(Pocket[] pockets)
{
    int number = Random.Shared.Next(0, pockets.Length);
    Color random = pockets[number].Color;
    return (number, random);
}

var option = RouletteOption.Straight;
switch (option)
{
    case RouletteOption.Straight:
        //int number = GetRouletteNumber();
        //(int randomNumber, Color randomColor) = GetRandomRoulette(pockets.Table);
        //
        //Console.WriteLine($"Roulette: {randomNumber}: {randomColor}");
        //Console.WriteLine(number == randomNumber
        //    ? "You win!" : "You lose!");
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
