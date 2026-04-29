using Casino.Domain;
using Casino.Services.Base;

namespace Casino.Services;

public class OutputService : IOutputService
{
    public void WriteMessage(string message) =>
        Console.WriteLine(message);

    public void WriteBoard(RouletteAggregate roulette)
    {
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
    }
}
