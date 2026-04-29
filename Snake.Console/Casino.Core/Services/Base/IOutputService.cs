using Casino.Domain;

namespace Casino.Services.Base;

public interface IOutputService
{
    void WriteBoard(RouletteAggregate roulette);
    void WriteMessage(string message);
}